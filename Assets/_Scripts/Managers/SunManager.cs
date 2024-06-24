using System;
using System.Collections;
using PVZ.Effect;
using UnityEngine;

namespace PVZ.Managers
{
    public class SunManager : MonoBehaviour
    {
        [SerializeField] private int currentSunNum = 100;
        [SerializeField] private Transform sunUI = null;

        [SerializeField] private GameObject sunPrefab = null;


        //阳光生成
        [Header("飘落阳光生成参数")]
        [SerializeField] float sunSpawnXRange = 10f;
        [SerializeField] float sunSpawnYPos = 10f;

        [SerializeField] float minSunFallDistance = 5;
        [SerializeField] float maxSunFallDistance = 10;

        [SerializeField] float fallingSpeed = 100f;

        float spawnInterval = 3f;
        float spawnTimer = 0;

        public event Action SunNumChanged;

        GameManager gameManager;

        void Awake()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
        }

        void Update()
        {
            if (gameManager.IsGameAlive == false) return;
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                Debug.Log("阳光飘落");
                SpawnSun();
                spawnTimer = 0;
            }
        }

        public bool UseSun(int cost)
        {
            if (cost > currentSunNum)
            {
                return false;
            }

            currentSunNum -= cost;
            SunNumChanged?.Invoke();
            return true;
        }

        public void AddSun(int num)
        {
            //Debug.Log("Add Sun");
            currentSunNum += num;
            SunNumChanged?.Invoke();
        }

        public void SpawnSun(Vector3 posToSpawn)
        {
            Transform sunTransform = Instantiate(sunPrefab, posToSpawn, Quaternion.identity).transform;

            Sun sun = sunTransform.GetComponent<Sun>();
            sun.Setup(this);
            // sun.StartFalling(landingPos);
        }

        //生成飘落的阳光
        private void SpawnSun()
        {
            float randomXPos = UnityEngine.Random.Range(-sunSpawnXRange - 100, sunSpawnXRange);
            Vector3 spawnPos = new Vector3(randomXPos, sunSpawnYPos, 0);
            Vector3 landingPos = new Vector3(randomXPos, sunSpawnYPos - UnityEngine.Random.Range(minSunFallDistance, maxSunFallDistance), 0);

            Transform sunTransform = Instantiate(sunPrefab, spawnPos, Quaternion.identity).transform;

            Sun sun = sunTransform.GetComponent<Sun>();
            sun.Setup(this);
            sun.StartFalling(landingPos);

        }

        public int GetCurrentSunNum() => currentSunNum;

        public Vector3 GetSunUIWorldPos()
        {
            Vector3 uiWorldPos = Camera.main.ScreenToWorldPoint(sunUI.position);
            uiWorldPos.z = 0;
            return uiWorldPos;
        }


    }
}


