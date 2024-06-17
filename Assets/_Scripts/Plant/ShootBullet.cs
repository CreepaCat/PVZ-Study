using PVZ.Sound;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BasePlant))]
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab = null;
        [SerializeField] private float shootInterval = 1f;
        [SerializeField] private Transform shootPos;

        private float lastShootTime = 0f;

        BasePlant basePlant;
        void Start()
        {
            basePlant = GetComponent<BasePlant>();
        }

        void Update()
        {


            if (!basePlant.IsPlanted()) return;

            //当前行有敌人时才发射子弹
            if (!basePlant.CanAttack())
            {
                return;

            }

            if (lastShootTime < shootInterval)
            {
                lastShootTime += Time.deltaTime;
            }
            else
            {
                Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                lastShootTime = 0f;
                SoundPlayer.Instance.PlaySound(GlobalPath.S_shoot);
            }
        }
    }
}


