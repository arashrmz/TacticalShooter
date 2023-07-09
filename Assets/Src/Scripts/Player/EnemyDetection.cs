﻿using System;
using System.Collections.Generic;
using Src.Scripts.Enemies;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class EnemyDetection : MonoBehaviour
    {
        [SerializeField] private float visionRange;
        [SerializeField] private float visionAngle;
        [SerializeField] private int visionConeResolution = 120;

        private List<Enemy> visibleEnemies = new List<Enemy>();
        private HashSet<Enemy> previouslyVisibleEnemies = new HashSet<Enemy>();

        void FixedUpdate()
        {
            visibleEnemies.Clear();

            // Get all colliders within range
            Collider[] colliders = Physics.OverlapSphere(transform.position, visionRange);
            
            foreach (Collider collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy == null || visibleEnemies.Contains(enemy)) continue;

                Vector3 directionToTarget = enemy.transform.position - transform.position;

                // Check if target object is within detection angle
                if (!(Vector3.Angle(transform.forward, directionToTarget) <= visionAngle / 2f)) continue;

                // Cast a ray towards target object
                if (Physics.Raycast(transform.position, directionToTarget.normalized, out var hit))
                {
                    // Check if nothing obstructs line of sight to enemy
                    if (hit.collider.gameObject == enemy.gameObject)
                    {
                        // Add the enemy to the list of visible enemies
                        visibleEnemies.Add(enemy);
                    }
                }
            }

            // Perform additional logic with the updated list of visible enemies

            foreach (var previouslyVisibleEnemy in previouslyVisibleEnemies)
            {
                if (!visibleEnemies.Contains(previouslyVisibleEnemy))
                {
                    Debug.Log(previouslyVisibleEnemy.name + " is no longer visible!");
                    // Handle logic for enemies that are no longer visible
                }
            }

            foreach (Enemy newlyVisibleEnemy in visibleEnemies)
            {
                if (!previouslyVisibleEnemies.Contains(newlyVisibleEnemy))
                {
                    Debug.Log(newlyVisibleEnemy.name + " is newly visible!");
                    // Handle logic for enemies that are newly visible
                }
            }

            previouslyVisibleEnemies.Clear();
            previouslyVisibleEnemies.UnionWith(visibleEnemies);
        }
    }
}