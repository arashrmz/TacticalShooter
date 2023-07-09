using System;
using EasyJoystick;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private Joystick rotationJoystick;
        
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            var movement = new Vector3(input.x, 0f, input.y) * moveSpeed;
            // Normalize the direction vector to prevent faster diagonal movements
            if (movement.magnitude > 0)
                movement.Normalize();

            // Move the player in local space relative to its own rotation
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        private void HandleRotation()
        {
            Vector2 offsetMousePos =
                new Vector2(rotationJoystick.Horizontal(), rotationJoystick.Vertical());
            if (offsetMousePos.sqrMagnitude < Mathf.Epsilon)
                return;
            float targetRotationY =
                Mathf.Atan2(offsetMousePos.x, offsetMousePos.y) *
                Mathf.Rad2Deg;

            Quaternion targetRotation =
                Quaternion.Euler(0f, targetRotationY , 0f);
         
            // transform.rotation= Quaternion.Lerp(transform.rotation,
            //     targetRotation,
            //     rotateSpeed * Time.deltaTime);

            transform.rotation = targetRotation;
        }
    }
}