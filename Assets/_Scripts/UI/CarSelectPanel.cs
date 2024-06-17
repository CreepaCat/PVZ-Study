using UnityEngine;

namespace PVZ.UI
{
    public class CarSelectPanel : MonoBehaviour
    {
        [SerializeField] Transform cardsContainer = null;
        [SerializeField] GameObject cardSelectItemPrefab = null;
        void Start()
        {
            for (var i = 0; i < 40; i++)
            {
                GameObject card = Instantiate(cardSelectItemPrefab, cardsContainer);
                card.name = "Card" + i;//定制名字，方便通过名字查找
            }
        }

        void Update()
        {

        }
    }
}


