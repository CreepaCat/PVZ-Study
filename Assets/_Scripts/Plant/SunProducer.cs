using Core;
using PVZ.Managers;
using UnityEngine;

namespace Plant
{
    [RequireComponent(typeof(BasePlant))]
    public class SunProducer : MonoBehaviour
    {
        [SerializeField] private float preparingTime = 5f;
        [SerializeField] private GameObject sunPrefab;

        [SerializeField] float sunPosOffset = 50;
        private float lastPreparedTime = 0;
        private Animator sunFlowerAnima;
        private bool isLighting = false;

        SunManager sunManager;
        //  public bool isPlanted = false;
        void Start()
        {
            sunFlowerAnima = GetComponent<Animator>();
            sunManager = GameObject.FindObjectOfType<SunManager>();
        }

        void Update()
        {
            if (!GetComponent<BasePlant>().IsPlanted()) return;

            lastPreparedTime += Time.deltaTime;

            if (lastPreparedTime > preparingTime && !isLighting)
            {
                isLighting = true;
                sunFlowerAnima.SetBool("isLight", true);
                Debug.Log("产生阳光");
            }

        }

        //动画事件 发光结束
        public void BornSunOver()
        {
            sunFlowerAnima.SetBool("isLight", false);
            //发光结束时产生阳光
            ProductSun();
            isLighting = false;
            lastPreparedTime = 0f;
        }

        private void ProductSun()
        {
            float randomPosX = Random.Range(0, 2) * 2 - 1;
            randomPosX *= sunPosOffset;
            Vector3 sunPos = transform.position + new Vector3(randomPosX, 0, 0);

            sunManager.SpawnSun(sunPos);
            // Instantiate(sunPrefab, sunPos, Quaternion.identity);
        }


    }
}


