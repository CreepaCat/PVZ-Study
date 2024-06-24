using UnityEngine.UI;
using PVZ.Level;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System;
using PVZ.Managers;

namespace PVZ.UI
{
    public class CarSelectPanel : MonoBehaviour
    {
        [SerializeField] Transform cardsContainer = null;
        [SerializeField] GameObject cardGridCellPrefab = null;
        [SerializeField] GameObject cardSelectItemPrefab = null;
        [SerializeField] PlantInfo plantInfo = null;

        List<ClickTarget> cards = new List<ClickTarget>();
        void Start()
        {
            InitialCardsSheet();
            RectTransform rectTransform = GetComponent<RectTransform>();
            transform.DOMoveY(rectTransform.position.y - rectTransform.sizeDelta.y, 2f).From().SetEase(Ease.OutQuad);

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnGameStart += OnGameStart;

        }

        private void InitialCardsSheet()
        {
            for (var i = 0; i < 40; i++)
            {
                //生成格子
                GameObject gridCell = Instantiate(cardGridCellPrefab, cardsContainer);
                gridCell.name = "CardCell" + i;//定制名字，方便通过名字查找
            }

            List<PlantInfoItem> plantInfoItems = plantInfo.GetPlantInfoItems();
            for (var i = 0; i < plantInfoItems.Count; i++)
            {
                //生成植物卡片
                GameObject card = InstantiateCard(i);

                ClickTarget clickTarget = card.GetComponent<ClickTarget>();
                clickTarget.isInSelectGrid = true;
                clickTarget.IsSelected = false;
                clickTarget.SetCardIndex(i);
                cards.Add(clickTarget);

            }

        }

        private GameObject InstantiateCard(int index)
        {
            Transform parent = cardsContainer.Find("CardCell" + index);
            GameObject card = Instantiate(cardSelectItemPrefab, parent);
            Image cardImage = card.GetComponent<Image>();
            cardImage.sprite = plantInfo.GetPlantInfoItems()[index].cardSprite;
            card.name = plantInfo.GetPlantInfoItems()[index].plantName;
            return card;
        }

        public void PutCardBack(int cardIndex, Vector3 originPos, Transform tempParent)
        {
            GameObject card = InstantiateCard(cardIndex);
            card.transform.SetParent(tempParent, false);

            card.GetComponent<ClickTarget>().enabled = false;
            card.transform.position = originPos;
            card.transform.DOMove(cardsContainer.Find("CardCell" + cardIndex).transform.position, 1f).OnComplete(

                () =>
                {
                    Destroy(card);
                    ClickTarget clickTarget = cards[cardIndex];
                    clickTarget.Unselect();
                }
            );


        }

        //游戏开始时隐藏选卡面板
        void OnGameStart()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            transform.DOMoveY(rectTransform.position.y - rectTransform.sizeDelta.y, 2f).SetEase(Ease.OutQuad).OnComplete(
                () => { gameObject.SetActive(false); }
            );
        }




    }
}


