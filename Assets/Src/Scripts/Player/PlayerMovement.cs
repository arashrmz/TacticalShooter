using System;
using UnityEngine;

namespace Src.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotateSpeed = 10f;

        private void Update()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            //handle movement
            var movement = new Vector3(input.x, 0f, input.y) * moveSpeed;
            // Normalize the direction vector to prevent faster diagonal movements
            if (movement.magnitude > 0)
                movement.Normalize();

            // Move the player in world space direction without considering rotation
            transform.Translate(movement * (moveSpeed * Time.deltaTime), Space.World);
            
            // Handle rotation based on mouse input or other means (e.g., touch drag)
            Vector3 mousePosition = Input.mousePosition;
            Vector3 playerScreenPos =
                Camera.main.WorldToScreenPoint(transform.position);

            Vector2 offsetMousePos =
                new Vector2(mousePosition.x - playerScreenPos.x,
                    mousePosition.y - playerScreenPos.y);

            float targetRotationY =
                Mathf.Atan2(offsetMousePos.x, offsetMousePos.y) *
                Mathf.Rad2Deg;

            Quaternion targetRotation =
                Quaternion.Euler(0f, targetRotationY, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }
}