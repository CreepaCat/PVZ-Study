using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PVZ.UI
{
    public class ClickTarget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] GameObject selectedMask = null;
        [SerializeField] public int cardIndex = 0;
        public bool isInSelectGrid = false;
        public bool IsSelected = false;
        void Start()
        {
            if (selectedMask != null)
                selectedMask.SetActive(IsSelected);
        }

        public void SetCardIndex(int index)
        {
            cardIndex = index;
        }

        public void Unselect()
        {
            IsSelected = false;
            if (selectedMask != null)
                selectedMask.SetActive(IsSelected);
        }




        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsSelected)
            {
                if (!isInSelectGrid)
                {
                    Destroy(gameObject);
                    GameObject.FindObjectOfType<CarSelectPanel>().PutCardBack(cardIndex, transform.position, transform.parent);
                    GameObject.FindObjectOfType<CardsBarUI>().RemoveCard(cardIndex);
                }
                return;
            }

            IsSelected = true;
            selectedMask.SetActive(IsSelected);
            GameObject.FindObjectOfType<CardsBarUI>().AddNewCard(cardIndex, transform.position);
        }

    }
}


