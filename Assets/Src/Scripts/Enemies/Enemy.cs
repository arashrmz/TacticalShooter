using System;
using UnityEngine;

namespace Src.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
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
    }
}