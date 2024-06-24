using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    [CreateAssetMenu(fileName = "PlantInfoSO", menuName = "PVZ/Level/PlantInfoSO")]
    public class PlantInfo : ScriptableObject
    {
        [SerializeField] public List<PlantInfoItem> plantInfoItemList = new List<PlantInfoItem>();

        public List<PlantInfoItem> GetPlantInfoItems()
        {
            return plantInfoItemList;
        }
    }

    [System.Serializable]
    public class PlantInfoItem
    {
        public int plantID;
        public string plantName;
        public string description;
        public int sunCost;
        public float plantingInterval;
        public Sprite cardSprite;
        public GameObject cardPrefab;

    }
}


