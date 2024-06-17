using UnityEditor;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using PVZ.Level;
using System;
using System.Reflection;

namespace PVZ.Editor
{
    [InitializeOnLoad]
    public class StartUp
    {
        static bool isCreated = true;
        static StartUp()
        {
            if (isCreated) return;
            string path = Application.dataPath + "/Editor/关卡管理.xlsx";
            string assetName = "Level01";

            //打开IO流
            FileInfo fileInfo = new FileInfo(path);
            //创建SO对象
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();

            //用IO流打开Excel
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["僵尸生成"];

                //从第三行开始是正式数据
                for (int i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    //创建行数据对象，并获取其type
                    LevelItem levelItem = new LevelItem();
                    Type itemType = typeof(LevelItem);

                    for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                    {
                        //获取格子数据打印到控制台
                        // Debug.Log("数据内容  " + worksheet.GetValue(i, j).ToString());

                        //通过反射将数据写入到SO对象
                        //Excel的第二行是变量名
                        FieldInfo variable = itemType.GetField(worksheet.GetValue(2, j).ToString());
                        //变量值
                        string tableValue = worksheet.GetValue(i, j).ToString();

                        //通过反射给变量赋值，注意变量类型转换
                        variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));
                    }

                    //每行读取完毕都将item添加到list
                    levelData.levelItems.Add(levelItem);

                }
                //文件流关闭后将SO对象保存
                //创建SO资产文件
                AssetDatabase.CreateAsset(levelData, "Assets/Resources/" + assetName + ".asset");
                AssetDatabase.SaveAssets(); //保存
                AssetDatabase.Refresh();   //刷新
            }
        }
    }
}


