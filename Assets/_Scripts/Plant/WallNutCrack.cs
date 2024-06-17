using Core;
using UnityEngine;

namespace Plant
{
    public class WallNutCrack : MonoBehaviour
    {

        Health health;
        Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            health.OnTakeDamage += OnTakeDamage;
        }

        void OnTakeDamage()
        {
            if (health.GetCurrentHealth() <= health.GetMaxHealth() * 0.3f)
            {
                animator.SetBool("Cracked2", true);
            }
            else if (health.GetCurrentHealth() <= health.GetMaxHealth() * 0.6f)
            {
                animator.SetBool("Cracked1", true);
            }
        }
    }
}


