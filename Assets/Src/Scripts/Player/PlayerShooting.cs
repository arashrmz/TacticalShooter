using System;
using Src.Scripts.Enemies;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        
        private EnemyDetection _enemyDetection;
        
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
        }

        private void OnDestroy()
        {
            _enemyDetection.OnDetectEnemy -= ShootEnemies;
        }
    }
}