using System;
using UnityEngine;

namespace Src.Scripts.Enemies
{   
    //checks enemy's line of sight to see if the player is in it
    public class EnemyLineOfSight : MonoBehaviour
    {
        [SerializeField] private float viewDistance = 7f;
        [SerializeField] private float viewAngle = 45f;

        private Transform _target;

        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            CheckLineOfSight();
        }
        
        //checks to see if target is in view cone
        private void CheckLineOfSight()
        {
            if (_target == null)
                return;
            
            Vector3 directionToPlayer = (_target.position - transform.position).normalized;
         
            if (Vector3.Angle(transform.forward, directionToPlayer) <= viewAngle * 0.5f)
            {    
                RaycastHit hit;
             
                if(Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
                {
                    if(hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player in sight");
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw the view cone gizmo in the Scene view
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

            Vector3 rightBoundary = Quaternion.Euler(0, viewAngle * 0.5f, 0) * transform.forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle * 0.5f, 0) * transform.forward;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
        
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        }
    }
}