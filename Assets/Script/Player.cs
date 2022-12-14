using UnityEngine;

namespace Script
{
    public class Player : MonoBehaviour
    {
        private PlayerController _controller;
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }

        void Update()
        {
            _controller.HandleJump();
            _controller.HandleMovement();
            _controller.HandleRotation();
            _controller.HandleAnimation();
            
        }
    }
}

