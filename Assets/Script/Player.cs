using UnityEngine;

namespace Script
{
    public class Player : MonoBehaviour
    {
        private PlayerController _controller;
        private void Awake() {
            _controller = GetComponent<PlayerController>();
        }

        private void Start() {
            Input.ResetInputAxes();
        }

        private void Update()
        {
            Move();
        }

        private void Move() {
            _controller.HandleAnimation();
            _controller.HandleRotation();
            _controller.HandleMovement();
        }
    }
}
