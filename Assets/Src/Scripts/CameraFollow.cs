using System;
using UnityEngine;

namespace Src.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 5f;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            Quaternion playerRotation = target.rotation;
            var desiredPosition = target.position +  playerRotation * offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            
            transform.LookAt(target);
        }
    }
}