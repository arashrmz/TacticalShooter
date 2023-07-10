using System;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 30;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        //take damage
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        //player died
        private void Die()
        {
            Debug.Log("You lost!");
        }
    }
}