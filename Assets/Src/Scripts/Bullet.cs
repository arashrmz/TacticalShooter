using System;
using Src.Scripts.Enemies;
using UnityEngine;

namespace Src.Scripts
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction; //direction bullet must travel
        private bool _isShot = false; //if the bullet is shot

        [SerializeField] private float moveSpeed = 10f; //move speed of the bullet
        [SerializeField] private int damage = 1; //damage given by the bullet
        [SerializeField] private float timeToLive = 5f;

        public void SetTarget(Transform target)
        {
            _direction = (target.position - transform.position).normalized;
            _isShot = true;
        }

        private void Update()
        {
            if (!_isShot)
                return;

            MoveBullet();
            CalculateTimeToLive();
        }

        //moves the bullet in its direction
        private void MoveBullet()
        {
            // Move the bullet towards the target position with a constant speed
            transform.Translate(_direction * (moveSpeed * Time.deltaTime));
        }

        //calculate how much time bullet has to live
        private void CalculateTimeToLive()
        {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //if hit the enemy
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (!other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}