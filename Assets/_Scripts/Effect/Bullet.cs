using Core;
using UnityEngine;

namespace PVZ.Effect
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        private GameObject shooter = null;

        public void SetBullet(GameObject shooter)
        {
            this.shooter = shooter;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ZombieBase>() != null)
            {
                Health health = other.GetComponent<Health>();
                health.ChangeHealth(-damage, shooter);
                Destroy(gameObject);
            }
        }
    }
}


