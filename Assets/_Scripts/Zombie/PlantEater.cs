using Core;
using PVZ.Sound;
using UnityEngine;

namespace PVZ.Zombie
{
    public class PlantEater : MonoBehaviour
    {

        [SerializeField] int eatingDamage = 10;
        [SerializeField] float damageInterval = 1.0f;
        [SerializeField] private float lastEatingTime = Mathf.Infinity;


        [SerializeField] private BasePlant targetPlant = null;
        private bool isEating = false;


        void Update()
        {
            if (!isEating) return;

            if (lastEatingTime >= damageInterval)
            {
                if (targetPlant != null)
                {

                    targetPlant.GetComponent<Health>().ChangeHealth(-eatingDamage, gameObject);
                    SoundPlayer.Instance.PlaySound(GlobalPath.S_chomp);
                }
                lastEatingTime = 0;
            }
            lastEatingTime += Time.deltaTime;
        }



        private void ChangeToWalkSate()
        {
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<MoveRight>().StopMovement(false);
            targetPlant = null;
            isEating = false;

            lastEatingTime = Mathf.Infinity; //重置伤害计时
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            BasePlant newTargetPlant = other.gameObject.GetComponent<BasePlant>();
            if (newTargetPlant != null)
            {
                targetPlant = newTargetPlant;
                GetComponent<Animator>().SetBool("Walk", false);
                GetComponent<MoveRight>().StopMovement(true);
                isEating = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            BasePlant basePlant = other.GetComponent<BasePlant>();
            if (basePlant != null && object.ReferenceEquals(basePlant, targetPlant))
            {
                Debug.Log("Trigger退出");
                ChangeToWalkSate();
            }
        }
    }
}


