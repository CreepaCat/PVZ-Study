using System;
using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float currentHealth;

        public event Action OnDie;
        public event Action OnHalf;

        public event Action OnTakeDamage;

        public float GetMaxHealth() => maxHealth;
        public float GetCurrentHealth() => currentHealth;
        void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {

        }

        public void ChangeHealth(int valueToChange, GameObject instigator)
        {
            if (currentHealth <= 0) return;


            currentHealth += valueToChange;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (valueToChange < 0)
            {
                OnTakeDamage?.Invoke();
            }


            if (currentHealth <= 0)
            {
                Debug.Log(gameObject.name + "死亡");
                // Destroy(gameObject);
                OnDie?.Invoke();
            }
            else if (currentHealth <= maxHealth * 0.5f)
            {
                OnHalf?.Invoke();
            }

        }
    }
}


