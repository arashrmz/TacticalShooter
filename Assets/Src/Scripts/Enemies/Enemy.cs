using System;
using UnityEngine;

namespace Src.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;
        private EnemyLineOfSight _lineOfSight;
        
        private void Start()
        {
            _currentHealth = maxHealth;
            _lineOfSight = GetComponent<EnemyLineOfSight>();
            _lineOfSight.OnPlayerEnterSight += PlayerOnSight;
            _lineOfSight.OnPlayerExitSight += PlayerExitSight;
        }
        
        //player is in sight of enemy (deal damage, ...)
        private void PlayerOnSight()
        {
            Debug.Log("Player spotted");
        }
        
        private void PlayerExitSight()
        {
            Debug.Log("Player out of sight!");
        }

        //hit by a bullet
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _lineOfSight.OnPlayerEnterSight -= PlayerOnSight;
            _lineOfSight.OnPlayerExitSight -= PlayerExitSight;
        }
    }
}