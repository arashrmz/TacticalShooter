using System;
using Src.Scripts.Enemies;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float shootingRate = 0.5f;
        
        private EnemyDetection _enemyDetection;
        private float _currentShootingRate = 0f;
        
        private void Start()
        {
            _enemyDetection = GetComponent<EnemyDetection>();
            _enemyDetection.OnDetectEnemy += ShootEnemies;
        }
        
        private void ShootEnemies()
        {
            //if no enemies in sight, return
            if(_enemyDetection.VisibleEnemies.Count == 0)
                return;

            _currentShootingRate += Time.deltaTime;

            if (_currentShootingRate < shootingRate)
                return;
            //get closest enemy
            var closestDistanceSqr = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (Enemy enemy in _enemyDetection.VisibleEnemies)
            {
                float distanceSqr = Vector3.SqrMagnitude(enemy.transform.position - transform.position);

                if(distanceSqr < closestDistanceSqr)
                { 
                    closestDistanceSqr = distanceSqr;
                    closestEnemy = enemy;              
                }           
            }
            
            //shoot at closest enemy
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetTarget(closestEnemy.transform);
            _currentShootingRate = 0f;
        }

        private void OnDestroy()
        {
            _enemyDetection.OnDetectEnemy -= ShootEnemies;
        }
    }
}