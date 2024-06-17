using PVZ.Managers;
using PVZ.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PVZ.UI
{
    public class CardItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image progress;
        [SerializeField] private Image dark;
        [SerializeField] private GameObject plantPrefab = null;

        [SerializeField] private float plantCD = 3f;

        [SerializeField] private int sunCost = 100;


        //CACHE
        // private int currentSunNum = 200;
        private bool isReady = true;
        private float timeFromLastPlanted = 0;

        SunManager sunManager = null;
        PlantingManager plantingManager = null;

        void Start()
        {
            dark.gameObject.SetActive(false);
            progress.fillAmount = 0;
            sunManager = GameObject.FindObjectOfType<SunManager>();
            plantingManager = GameObject.FindObjectOfType<PlantingManager>();
        }

        void Update()
        {
            UpdateCardState();

            timeFromLastPlanted += Time.deltaTime;


        }

        private void UpdateCardState()
        {
            if (sunManager.GetCurrentSunNum() >= sunCost && timeFromLastPlanted >= plantCD)
            {
                isReady = true;
                dark.gameObject.SetActive(false);
                progress.fillAmount = 0;
            }
            else
            {
                isReady = false;
                dark.gameObject.SetActive(true);
                progress.fillAmount = 1 - Mathf.Clamp01(timeFromLastPlanted / plantCD);

            }
        }

        public GameObject GetPlantGO()
        {
            return Instantiate(plantPrefab, transform.position, Quaternion.identity);
        }




        //点击拿去植物
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isReady) return;

            //planting状态下不能拿取新卡牌
            if (plantingManager.isPlanting) return;

            plantingManager.SetCurrentPlant(GetPlantGO());
            plantingManager.OnPlanted += OnPlanted;

            SoundPlayer.Instance.PlaySound(GlobalPath.S_buttonclick);
        }

        private void OnPlanted()
        {
            timeFromLastPlanted = 0f;
            sunManager.UseSun(sunCost);
            plantingManager.OnPlanted -= OnPlanted;
            SoundPlayer.Instance.PlaySound(GlobalPath.S_plant);
        }

    }
}


