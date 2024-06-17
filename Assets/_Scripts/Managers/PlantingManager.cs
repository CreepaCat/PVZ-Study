using System;
using Core;
using UnityEngine;

namespace PVZ.Managers
{
    public class PlantingManager : MonoBehaviour
    {

        [SerializeField] private Grid plantingGrid = null;
        [SerializeField] GameObject mouseIndicator = null;
        //  [SerializeField] GameObject gridSquareIndicator = null;
        [SerializeField] GameObject cellPosIndicator = null;


        //CACHE
        private BasePlant currentPlant = null;

        private GameObject currentPlantPreview = null;

        public bool isPlanting = false;

        public event Action OnPlanted;

        public void SetCurrentPlant(GameObject plantGO)
        {

            currentPlant = plantGO.GetComponent<BasePlant>();

            //植物虚影
            currentPlantPreview = GameObject.Instantiate(plantGO);
            SpriteRenderer plantSprite = currentPlantPreview.GetComponent<SpriteRenderer>();
            Color shadowColor = plantSprite.color;
            shadowColor.a = 0.5f;
            plantSprite.color = shadowColor;

            isPlanting = true;


        }

        void Update()
        {
            if (currentPlant != null && isPlanting)
            {
                currentPlant.transform.position = GetMousePosInWorld();

                //获取鼠标在Grid里的坐标
                Vector3Int mousePosInGrid = plantingGrid.WorldToCell(GetMousePosInWorld());

                //将格子坐标限制在有效区域内
                mousePosInGrid.x = Mathf.Clamp(mousePosInGrid.x, -4, 4);
                mousePosInGrid.y = Mathf.Clamp(mousePosInGrid.y, -2, 2);
                Vector3 squareCenter = plantingGrid.GetCellCenterWorld(mousePosInGrid);


                //可视化
                mouseIndicator.transform.position = GetMousePosInWorld();
                //gridSquareIndicator.transform.position = squareCenter;
                currentPlantPreview.transform.position = squareCenter;


                //放置植物
                if (Input.GetMouseButtonUp(0))
                {
                    currentPlant.PlantOnPos(currentPlant.gameObject, squareCenter);
                    // currentPlant.transform.position = squareCenter;
                    Destroy(currentPlantPreview);
                    isPlanting = false;
                    //planting后该卡牌进入冷却
                    OnPlanted?.Invoke();

                }

                //右键取消放置状态
                else if (Input.GetMouseButtonDown(1))
                {
                    Destroy(currentPlant.gameObject);
                    Destroy(currentPlantPreview);
                    currentPlant = null;
                    currentPlantPreview = null;

                    isPlanting = false;

                }
            }

        }


        private Vector3 GetMousePosInWorld()
        {
            //TODO:鼠标在UI上时不更新位置，也不能放置植物

            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = Camera.main.nearClipPlane;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseInput);
            return mousePos;
        }


    }
}


