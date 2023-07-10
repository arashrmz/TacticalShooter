using System;
using UnityEngine;

namespace Src.Scripts.Enemies
{
    //checks enemy's line of sight to see if the player is in it
    public class EnemyLineOfSight : MonoBehaviour
    {
        [SerializeField] private float viewDistance = 7f;
        [SerializeField] private float viewAngle = 45f;
        [SerializeField] private LayerMask osbtaclesLayer;
        
        private Transform _target;
        private bool _playerInSight; //if player is in sight

        public event Action OnPlayerEnterSight; //event called when player enters the sight
        public event Action OnPlayerExitSight; //event called when player is no longer in sight

        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            var isCurrentlyInSight = CheckLineOfSight();
            
            if (isCurrentlyInSight && !_playerInSight)
            {
                //player just entered the sight
                _playerInSight = true;
                OnPlayerEnterSight?.Invoke();
            }
            else if (!isCurrentlyInSight && _playerInSight)
            {
                //player just exited the sight
                _playerInSight = false;
                OnPlayerExitSight?.Invoke();
            }
        }

        //checks to see if target is in view cone
        private bool CheckLineOfSight()
        {
            if (_target == null)
                return false;

            Vector3 directionToPlayer = (_target.position - transform.position).normalized;

            //if player is in view angle
            if (Vector3.Angle(transform.forward, directionToPlayer) <= viewAngle * 0.5f)
            {
                RaycastHit hit;

                //if player is in sight
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance, osbtaclesLayer))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }

            return false;
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