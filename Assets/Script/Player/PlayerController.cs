using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _inputs;
        private CharacterController _controller;
        private Animator _animator;

        private Vector2 _currentMovementInput;
        private Vector3 _currentMovement;
        private bool _isMovementPressed;

        void OnMovementInput(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
            _currentMovement.x = _currentMovementInput.x;
            _currentMovement.z = _currentMovementInput.y;
            _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        }

        void handleAnimation()
        {
            bool isDead = _animator.GetBool("isDead");
            bool isShooting = _animator.GetBool("isShooting");

            _animator.SetFloat("speed", _currentMovement.magnitude);
        }

        void Awake()
        {
            _inputs = new PlayerInput(); // Instanciating new inputs maps
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();

            _inputs.CharacterControls.Move.started += OnMovementInput;
            _inputs.CharacterControls.Move.canceled += OnMovementInput;
            _inputs.CharacterControls.Move.performed += OnMovementInput;
        }

        void Update()
        {
            handleAnimation();
            _controller.Move(_currentMovement * Time.deltaTime);
        }

        void OnEnable()
        {
            _inputs.CharacterControls.Enable();
        }

        void OnDisable()
        {
            _inputs.CharacterControls.Disable();
        }
    }
}
