using UnityEngine;

namespace Core
{
    public class DestroyInTime : MonoBehaviour
    {
        [SerializeField] private float destroyTimer = 5f;
        void Start()
        {
            Destroy(gameObject, destroyTimer);
        }


    }
}


