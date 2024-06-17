using System;
using System.Collections.Generic;
using PVZ.Managers;
using UnityEngine;

namespace Core
{
    public class ZombieBase : MonoBehaviour
    {

        [SerializeField] GameObject headToLost = null;

        void Start()
        {
            GetComponent<Health>().OnDie += OnDie;
            GetComponent<Health>().OnHalf += OnHalfHealth;
            headToLost.SetActive(false);

        }
        void OnDisable()
        {
            GetComponent<Health>().OnDie -= OnDie;
        }

        void Update()
        {

        }

        private void OnHalfHealth()
        {
            GetComponent<Animator>().SetBool("LostHead", true);

            //GameObject head = Instantiate(headToLost, transform.position, headToLost.transform.rotation);
            headToLost.transform.SetParent(null);
            headToLost.SetActive(true);
            Destroy(headToLost, 2f);
        }

        private void OnDie()
        {
            GetComponent<Animator>().SetTrigger("Die");
            //关闭碰撞盒子
            GetComponent<BoxCollider2D>().enabled = false;
            //停止移动
            GetComponent<MoveRight>().StopMovement(true);

            //从spawnManager中移除自己
            SpawnManager spawnManager = GameObject.FindObjectOfType<SpawnManager>();
            spawnManager.RemoveZombie(GetComponent<Health>());

        }

        //动画事件
        public void DestroySelf()
        {
            Destroy(gameObject, 2f);
        }

        // internal void Setup(List<Health> zombieList)
        // {
        //     throw new NotImplementedException();
        // }
    }
}


