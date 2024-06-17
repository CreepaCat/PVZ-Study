using PVZ.Managers;
using TMPro;
using UnityEngine;


namespace PVZ.UI
{
    public class CardsBarUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI sunNum;

        SunManager sunManager = null;
        void Start()
        {
            sunManager = GameObject.FindObjectOfType<SunManager>();
            sunManager.SunNumChanged += OnSunNumChanged;
        }

        void OnSunNumChanged()
        {
            sunNum.text = sunManager.GetCurrentSunNum().ToString();
        }
    }
}


