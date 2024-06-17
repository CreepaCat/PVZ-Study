using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    public class LevelData : ScriptableObject
    {
        [SerializeField] public List<LevelItem> levelItems = new List<LevelItem>();
    }

    [System.Serializable]
    public class LevelItem
    {
        public int ID;
        public int levelID;
        public int waveID;
        public float spawnTime;
        public int zombieType;
        public int spawnLine;

    }
}


