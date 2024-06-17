using System;
using UnityEngine;

namespace Core
{
    public class BasePlant : MonoBehaviour
    {
        private bool isPlanted = false;
        private bool canAttack = false;

        public bool IsPlanted() => isPlanted;

        public void Start()
        {
            GetComponent<Health>().OnDie += OnDie;
            //关闭碰撞器，种植后开启
            GetComponent<BoxCollider2D>().enabled = false;
        }

        void OnDisable()
        {
            GetComponent<Health>().OnDie -= OnDie;
        }
        public void PlantOnPos(GameObject plantGO, Vector3 pos)
        {
            isPlanted = true;
            GetComponent<BoxCollider2D>().enabled = true;
            plantGO.transform.position = pos;
        }

        private void OnDie()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 0.15f);
        }

        public void SetCanAttack(bool canAttack)
        {
            this.canAttack = canAttack;
        }

        public bool CanAttack()
        {
            return canAttack;
        }
    }
}


