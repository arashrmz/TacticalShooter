﻿using System;
using Src.Scripts.Enemies;
using UnityEngine;

namespace Src.Scripts
{
    public class Bullet : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _direction;
        
        [SerializeField] private float moveSpeed = 10f; //move speed of the bullet
        [SerializeField] private int damage = 1;    //damage given by the bullet
        
        public void SetTarget(Transform target)
        {
            _direction = (target.position - transform.position).normalized;
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
                return;
            
            // Move the bullet towards the target position with a constant speed
            transform.Translate(_direction * moveSpeed * Time.deltaTime);
            // Check if we have reached or passed our target position and handle it accordingly.
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);
        
            if (distanceToTarget <= 0.1f) 
            { 
                HitTarget();
            }
        }

        private void HitTarget()
        {
            //damage the enemy
            _target.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}