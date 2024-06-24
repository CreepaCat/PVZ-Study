using System;
using System.Collections.Generic;
using PVZ.UI;
using UnityEngine;

namespace PVZ.UISystem
{
    public class UIManager
    {
        //数据结构设计：
        //一个内部类记录UI元素的相关信息
        class UIElement
        {
            public string Resource; //预制体路径
            public bool IsCache;    //打开后是否缓存（销毁还是设为不可见）
            public GameObject Instance;  //记录其GO，用于开关
            public bool isClosed = true; //是否是关闭状态

        }
        //字典存储UI预制体
        Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

        //单例模式，可以不是mono单例
        static UIManager _instance = null;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIManager();

                }

                return _instance;

            }
        }

        //Unity快速启动模式 对静态变量的初始化
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            _instance = null;
        }

        //构造函数初始化
        public UIManager()
        {

            //测试，UI预制体要放在Resources的相对文件夹里
            this.UIResources.Add(typeof(UITest), new UIElement() { Resource = "UI/UITest", IsCache = true });
            this.UIResources.Add(typeof(UITest2), new UIElement() { Resource = "UI/UITest2", IsCache = false });
            Debug.Log("UIManager初始化完毕");
        }

        //显示窗口UI
        public T Show<T>()
        {
            Debug.Log("UIManager Show " + typeof(T));
            //SoundManager.Instance.PlaySound()
            Type type = typeof(T);

            if (this.UIResources.ContainsKey(type))//以UI名做key
            {
                UIElement info = this.UIResources[type];

                if (info.Instance != null) //每次都要判断其UI示例是否存在
                {
                    info.Instance.SetActive(true);//显示
                }
                else
                {
                    UnityEngine.Object prefab = Resources.Load(info.Resource);//代码读取预制体
                    if (prefab == null)
                    {
                        return default(T);
                    }
                    //强制类型转换：GameObject继承自Object
                    info.Instance = GameObject.Instantiate(prefab, UIMain.Instance.transform) as GameObject;//实例化预制体

                }
                info.isClosed = !info.isClosed;
                return info.Instance.GetComponent<T>();//用泛型统一管理
            }
            return default(T);
        }

        public void Close(Type type)
        {
            //SoundManager.Instance.PlaySound()

            if (this.UIResources.ContainsKey(type))
            {
                UIElement info = this.UIResources[type];
                if (info.IsCache)//如果要缓存
                {
                    info.Instance.SetActive(false);
                }
                else
                {
                    GameObject.Destroy(info.Instance);//如果不缓存，则直接销毁
                    info.Instance = null;
                }
                info.isClosed = !info.isClosed;
            }
        }

        //用一个按钮控制UI开关
        public void OnButtonClick<T>()
        {
            Type type = typeof(T);
            if (this.UIResources.ContainsKey(type))
            {
                UIElement info = this.UIResources[type];
                if (info.isClosed)
                {
                    Show<T>();
                }
                else
                {
                    Debug.LogFormat("通过按钮点击关闭窗口:{0}", info.Instance.GetComponent<UIWindow>().name);

                    info.Instance.GetComponent<UIWindow>().OnClickClose();

                }

            }

        }


    }
}


