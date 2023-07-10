using System;
using UnityEngine;

namespace Src.Scripts.Enemies
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private float shootingRate = 0.7f;
        [SerializeField] private GameObject bulletPrefab;

        private float _currentShootingRate = 0f;
        private bool _isShooting = false;
        private Transform _player;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        public void StartShooting()
        {
            _isShooting = true;
            _currentShootingRate = 0f;
        }

        public void StopShooting()
        {
            _isShooting = false;
        }
        
        private void FixedUpdate()
        {
            if (_isShooting)
                Shoot();
        }

        private void Shoot()
        {
            //check for fire rate
            _currentShootingRate += Time.deltaTime;
            if (_currentShootingRate < shootingRate)
                return;
            //reset the firing rate
            _currentShootingRate = 0f;
            
            //spawn the bullet
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetTarget(_player.transform);
        }
    }
}