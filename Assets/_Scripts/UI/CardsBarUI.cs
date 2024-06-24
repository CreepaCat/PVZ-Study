using PVZ.Managers;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using PVZ.Level;
using System;
using System.Globalization;


namespace PVZ.UI
{
    public class CardsBarUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI sunNum;
        [SerializeField] Transform cardsContainer = null;
        [SerializeField] Transform cardCellPrefab = null;
        [SerializeField] Transform cardItemPrefab = null;

        [SerializeField] PlantInfo plantInfos = null;

        SunManager sunManager = null;
        [SerializeField] List<ClickTarget> selectedCards = new();
        void Start()
        {
            sunManager = GameObject.FindObjectOfType<SunManager>();
            sunManager.SunNumChanged += OnSunNumChanged;

            InitialCardCells();

            RectTransform rectTransform = GetComponent<RectTransform>();
            transform.DOMoveY(rectTransform.position.y + rectTransform.sizeDelta.y, 2f).From().SetEase(Ease.OutQuad);


            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnGameStart += OnGameStart;
        }

        private void InitialCardCells()
        {
            for (var i = 0; i < 8; i++)
            {
                Transform barCell = Instantiate(cardCellPrefab, cardsContainer);
                barCell.name = "CardCell" + i;
            }
        }

        public void AddNewCard(int index, Vector3 originPos)
        {

            List<PlantInfoItem> plantInfoItems = plantInfos.GetPlantInfoItems();

            // Vector3 originPos = transform.position;

            Transform parent = cardsContainer.Find("CardCell" + selectedCards.Count);
            Vector3 targetPos = parent.position;

            Transform card = Instantiate(cardItemPrefab, parent);
            CardItem cardItem = card.GetComponent<CardItem>();

            PlantInfoItem plantItem = plantInfoItems[index];

            cardItem.Setup(plantItem.cardPrefab, plantItem.plantingInterval, plantItem.sunCost);


            //  card.transform.SetParent(transform);
            card.GetComponent<ClickTarget>().enabled = false;
            card.position = originPos;

            card.GetComponent<Image>().sprite = plantInfoItems[index].cardSprite;

            card.DOMove(targetPos, 1f).OnComplete(
                () =>
                {
                    card.GetComponent<ClickTarget>().enabled = true;
                }
            ); //UI动画
            ClickTarget clickTarget = card.GetComponent<ClickTarget>();
            clickTarget.isInSelectGrid = false;
            clickTarget.IsSelected = true;
            clickTarget.SetCardIndex(index);


            selectedCards.Add(clickTarget);
        }

        void OnSunNumChanged()
        {
            sunNum.text = sunManager.GetCurrentSunNum().ToString();
        }

        public void RemoveCard(int cardIndex)
        {
            //将该位置卡片移除
            int index = -1;
            for (var i = 0; i < selectedCards.Count; i++)
            {
                if (selectedCards[i].cardIndex == cardIndex)
                {
                    selectedCards.Remove(selectedCards[i]);
                    index = i;
                }
            }
            //将后面的卡片向前补位
            if (index > -1)
            {
                for (var i = 0; i < selectedCards.Count; i++)
                {
                    if (i >= index)
                    {
                        //UI动画
                        Transform parent = cardsContainer.Find("CardCell" + i);
                        selectedCards[i].transform.SetParent(parent);
                        selectedCards[i].transform.DOMove(parent.position, 0.5f);
                    }
                }


            }

        }

        void OnGameStart()
        {
            foreach (ClickTarget clickTarget in selectedCards)
            {
                clickTarget.enabled = false;
            }
        }
    }
}


