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
            _controller.HandleMovement();
            _controller.HandleAnimation();
            _controller.HandleAnimation();
            
            if (Input.GetButtonDown("Jump")) {
                _controller.Jump();
            }
        }
    }
}

