using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace PVZ.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject zombiePrefab = null;
        [SerializeField] float spawnInterval = 2f;
        float xCenter = 600;
        float yOffset = 92;
        Vector3[] spawnPosArray = null;

        public event Action<int> OnZombieNumChange;

        int zombieId = 0;

        [SerializeField] List<Health> currentWaveZombieList;

        public event Action OnZombieClear;
        void Start()
        {
            currentWaveZombieList = new List<Health>();

            InitSpawnPos();
        }

        public void SpawnZombieByTable(int zombieType, int lineIndex)
        {
            Debug.Log("通过配置表生成僵尸");
            InstantiateZombie(lineIndex);
        }


        private void InitSpawnPos()
        {
            spawnPosArray = new Vector3[5];
            Vector3 posLine1 = new Vector3(xCenter, yOffset * 2, 0);
            for (int i = 0; i < spawnPosArray.Length; i++) //从上往下排列，上面图层的layer不应遮挡下面的
            {
                spawnPosArray[i] = new Vector3(posLine1.x, posLine1.y - yOffset * i, 0);
                // Debug.Log($"第{i}行生成点" + spawnPosArray[i]);
            }
        }

        private void InstantiateZombie(int lineIndex)
        {
            Vector3 spawnPos = spawnPosArray[lineIndex];
            GameObject zombie = Instantiate(zombiePrefab, spawnPos, zombiePrefab.transform.rotation);
            currentWaveZombieList.Add(zombie.GetComponent<Health>());
            OnZombieNumChange?.Invoke(1);

            SpriteRenderer spriteRenderer = zombie.GetComponent<SpriteRenderer>();

            //防遮挡,位置越下面的僵尸显示在越上层；越后生成的僵尸显示在越上层。
            zombieId++;
            if (zombieId >= 100)
            {
                zombieId -= 100;
            }
            int sortingOrderNum = lineIndex * 100 + zombieId;

            spriteRenderer.sortingOrder = sortingOrderNum;
        }

        public void RemoveZombie(Health zombie)
        {
            currentWaveZombieList.Remove(zombie);
            // OnZombieNumChange?.Invoke(-1);

            if (currentWaveZombieList.Count == 0)
            {
                OnZombieClear?.Invoke();
            }
        }


    }
}


