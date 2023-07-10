using System;
using System.Collections;
using System.Threading.Tasks;
using Src.Scripts.Enemies;
using Src.Scripts.Weapons;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        
        private EnemyDetection _enemyDetection;
        private float _currentShootingRate = 0f;
        private Weapon _testWeapon;
        private bool _reloading = false;
        
        private void Start()
        {
            //create a test weapon
            _testWeapon = new Weapon
            {
                name = "Pistol",
                fireRate = 0.5f,
                magazineCapacity = 5,
                currentAmmoInMagazine = 5,
                totalBullets = 50,
                damage = 1,
                reloadTime = 2000
            };
            
            _enemyDetection = GetComponent<EnemyDetection>();
            _enemyDetection.OnDetectEnemy += ShootEnemies;
        }
        
        private void ShootEnemies()
        {
            //if no enemies in sight, return
            if(_enemyDetection.VisibleEnemies.Count == 0)
                return;

            //weapon cooldown
            _currentShootingRate += Time.deltaTime;
            if (_currentShootingRate < _testWeapon.fireRate)
                return;
            
            //check bullets in mag
            if (!_testWeapon.HasAmmoInMag())
            {
                Reload();
                return;
            }
                
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
            _testWeapon.Fire();
            _currentShootingRate = 0f;
        }

        async void Reload()
        {
            //check if already reloading
            if (_reloading)
                return;
            Debug.Log("Reloading!");
            _reloading = true;
            await Task.Delay(_testWeapon.reloadTime);
            _testWeapon.Reload();
            Debug.Log("Reloaded!");
        }
        
        private void OnDestroy()
        {
            _enemyDetection.OnDetectEnemy -= ShootEnemies;
        }
    }
}