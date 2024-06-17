using System;
using System.Collections;
using PVZ.Level;
using PVZ.Sound;
using UnityEngine;

namespace PVZ.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public LevelData levelData; //管理僵尸生成
        public LevelInfo levelInfo; //关卡波次信息

        //  public int[] levelsWave;

        public int currentLevelID;
        public int currentWaveID;
        SpawnManager spawnManager = null;


        public bool hasNextWave = true;
        public int currentWaveZombieTotalNum;

        public event Action OnNewWave;

        void Awake()
        {
            ReadTable();
        }
        void Start()
        {
            spawnManager = GameObject.FindObjectOfType<SpawnManager>();
            spawnManager.OnZombieClear += OnZombieClear;

        }

        void ReadTable()
        {
            StartCoroutine(LoadTableCR());
        }
        IEnumerator LoadTableCR()
        {
            ResourceRequest request = Resources.LoadAsync("Level01");
            ResourceRequest request2 = Resources.LoadAsync("LevelInfoSO");
            yield return request;
            yield return request2;
            levelData = request.asset as LevelData;
            levelInfo = request2.asset as LevelInfo;

            Debug.Log("关卡配置表读取完毕");
            // yield return new WaitForSeconds(1);
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.GameStart();
            SpawnZombieFromTable();

        }

        public void SpawnZombieFromTable()
        {
            //每次进行僵尸生成都将有下一波设为false
            hasNextWave = false;
            for (var i = 0; i < levelData.levelItems.Count; i++)
            {
                LevelItem levelItem = levelData.levelItems[i];
                if (currentLevelID == levelItem.levelID && currentWaveID == levelItem.waveID)
                {
                    currentWaveZombieTotalNum++;
                    StartCoroutine(SpawnZombieCR(levelItem));
                    hasNextWave = true;
                }
            }


            //僵尸消灭完后进入下一波，当最后一波消灭完后本关结束

            if (hasNextWave == false)
            {
                Debug.Log("没有下一波，恭喜通关");
                SoundPlayer.Instance.PlaySound(GlobalPath.S_winmusic);
            }
            else
            {
                Debug.Log("下一波");
                SoundPlayer.Instance.PlaySound(GlobalPath.S_awooga);

            }
        }

        IEnumerator SpawnZombieCR(LevelItem levelItem)
        {

            yield return new WaitForSeconds(levelItem.spawnTime);

            spawnManager.SpawnZombieByTable(levelItem.zombieType, levelItem.spawnLine);
        }

        void OnZombieClear()
        {
            currentWaveID++;
            SpawnZombieFromTable();
            OnNewWave?.Invoke();

        }

        public LevelInfoItem GetCurrentLevelInfoItem()
        {
            foreach (var item in levelInfo.levelInfoItemList)
            {
                if (item.levelID == currentLevelID)
                {
                    return item;
                }
            }
            return null;
        }
    }
}


