using System.Collections.Generic;
using Core;
using UnityEngine;

namespace PVZ.Lines
{
    public class Line : MonoBehaviour
    {

        [SerializeField] List<BasePlant> plantList;
        [SerializeField] List<ZombieBase> zombieList;
        void Start()
        {
            plantList = new List<BasePlant>();
            zombieList = new List<ZombieBase>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                Debug.Log("植物放进" + gameObject.name);
                plantList.Add(other.GetComponent<BasePlant>());
                if (zombieList.Count > 0)
                {
                    //当植物放进时，若行内有僵尸，通知其攻击
                    other.GetComponent<BasePlant>().SetCanAttack(true);
                }
            }

            if (other.gameObject.CompareTag("Zombie"))
            {
                Debug.Log("僵尸进入" + gameObject.name);
                zombieList.Add(other.GetComponent<ZombieBase>());
                //当僵尸进入时，通知行内所有植物攻击
                SetPlantsAttackStatus(true);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Plant"))
            {
                plantList.Remove(other.GetComponent<BasePlant>());
            }

            if (other.gameObject.CompareTag("Zombie"))
            {
                zombieList.Remove(other.GetComponent<ZombieBase>());

                if (zombieList.Count == 0)
                {
                    //当僵尸清空时，通知行内所有植物停止攻击
                    SetPlantsAttackStatus(false);
                }
            }
        }

        private void SetPlantsAttackStatus(bool canAttack)
        {
            foreach (BasePlant plant in plantList)
            {
                plant.SetCanAttack(canAttack);
            }
        }
    }
}


