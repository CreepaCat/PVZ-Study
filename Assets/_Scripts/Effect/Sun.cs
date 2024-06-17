using System;
using System.Collections;
using PVZ.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PVZ.Effect
{
    public class Sun : MonoBehaviour
    {

        [SerializeField] private int sunNum = 25;

        SunManager sunManager = null;

        public bool isClicked = false;

        float fallingSpeed = 100f;

        public void Setup(SunManager sunManager)
        {
            this.sunManager = sunManager;
        }


        void OnMouseDown()
        {
            isClicked = true;
            // sunManager = GameObject.FindObjectOfType<SunManager>();
            StartCoroutine(SunCollectionAnimaCR());

        }

        //阳光收集动画
        IEnumerator SunCollectionAnimaCR()
        {
            Vector3 destination = sunManager.GetSunUIWorldPos();
            Vector3 moveDir = (destination - transform.position).normalized;
            moveDir.z = 0;

            float currentDistance = Vector3.Distance(destination, transform.position);

            while (currentDistance > 20f)
            {
                yield return new WaitForEndOfFrame();
                transform.Translate(moveDir * 1000f * Time.deltaTime);
                currentDistance = Vector3.Distance(destination, transform.position);
                // Debug.Log("当前阳光与UI的距离：" + currentDistance);

            }

            sunManager.AddSun(sunNum);
            Destroy(gameObject);
        }

        //阳光飘落动画
        IEnumerator SunFallingAnimaCR(Vector3 endPos)
        {
            Vector3 moveDir = (endPos - transform.position).normalized;
            moveDir.z = 0;

            float currentDistance = Vector3.Distance(endPos, transform.position);

            while (currentDistance > 5f && isClicked == false) //如果阳光被点击，则跳出协程
            {
                yield return new WaitForEndOfFrame();
                transform.Translate(moveDir * fallingSpeed * Time.deltaTime);
                currentDistance = Vector3.Distance(endPos, transform.position);

            }
        }

        public void StartFalling(Vector3 landingPos)
        {
            StartCoroutine(SunFallingAnimaCR(landingPos));
        }
    }
}


