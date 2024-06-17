using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    [CreateAssetMenu(fileName = "LevelInfoSO", menuName = "PVZ/Level/LevelInfoSO")]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] public List<LevelInfoItem> levelInfoItemList = new List<LevelInfoItem>();
    }

    [System.Serializable]
    public class LevelInfoItem
    {
        public int levelID;
        public string levelName;

        public float[] progressPercent;
    }
}


