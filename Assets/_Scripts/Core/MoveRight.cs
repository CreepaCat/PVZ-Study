using UnityEngine;

namespace Core
{
    public class MoveRight : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        [SerializeField] private bool flipDir = false;

        //CACHE
        Vector3 direction;
        float movementSpeed;
        void Start()
        {
            direction = flipDir ? -Vector3.right : Vector3.right;
            movementSpeed = speed;
        }

        void Update()
        {

            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }

        public void StopMovement(bool isStop)
        {
            movementSpeed = isStop ? 0 : speed;
        }
    }
}


