using System;
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
        [SerializeField] private LayerMask obstacleLayers;
        
        private HashSet<Enemy> previouslyVisibleEnemies = new HashSet<Enemy>();
        public List<Enemy> VisibleEnemies { get; private set; } = new List<Enemy>();
        public event Action OnDetectEnemy;
        
        void FixedUpdate()
        {
            VisibleEnemies.Clear();

            // Get all colliders within range
            Collider[] colliders = Physics.OverlapSphere(transform.position, visionRange);
            
            foreach (Collider collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy == null || VisibleEnemies.Contains(enemy)) continue;

                Vector3 directionToTarget = enemy.transform.position - transform.position;

                // Check if target object is within detection angle
                if (!(Vector3.Angle(transform.forward, directionToTarget) <= visionAngle / 2f)) continue;
                int layerMask = ~obstacleLayers.value;
                Ray ray = new Ray(transform.position, directionToTarget.normalized);
                RaycastHit hit;

                // Cast a ray towards target object
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawLine(transform.position, hit.transform.position);
                    // Check if nothing obstructs line of sight to enemy
                    if (hit.collider.gameObject == enemy.gameObject)
                    {
                        // Add the enemy to the list of visible enemies
                        VisibleEnemies.Add(enemy);
                    }
                }
            }

            Debug.Log(VisibleEnemies.Count);
            OnDetectEnemy?.Invoke();
        }
    }
}