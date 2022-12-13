using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _inherentSpeedFactor;
        [SerializeField] private float _inherentRotationFactor;
        // [SerializeField] private float _inherentRunSpeed;

        private PlayerInput _inputs;
        private Animator _animator;
        private CharacterController _character;
        private CameraController _camera;

        private Vector2 _rawInput;
        private Vector3 _motion;
        private bool _isRunning;

        void Awake()
        {
            _inputs = new PlayerInput();
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterController>();
            _camera = GameObject.Find("MainCamera").GetComponent<CameraController>();

            registerInputsCallbacks(_inputs);
        }

        void OnEnable()
        {
            _inputs.CharacterControls.Enable();
        }

        void OnDisable()
        {
            _inputs.CharacterControls.Disable();
        }

        void Update()
        {
            handleRotation();
            handleAnimation();
            _character.Move(_motion * _inherentSpeedFactor * Time.deltaTime);
        }

        private void registerInputsCallbacks(PlayerInput _inputs)
        {
            _inputs.CharacterControls.Move.started += OnMovementInput;
            _inputs.CharacterControls.Move.canceled += OnMovementInput;
            _inputs.CharacterControls.Move.performed += OnMovementInput;

            _inputs.CharacterControls.Rotation.started += OnRotationInput;
            _inputs.CharacterControls.Rotation.canceled += OnRotationInput;
            _inputs.CharacterControls.Rotation.performed += OnRotationInput;

            _inputs.CharacterControls.Run.started += OnRunInput;
            _inputs.CharacterControls.Run.canceled += OnRunInput;
            _inputs.CharacterControls.Run.performed += OnRunInput;

            _inputs.CharacterControls.Jump.started += OnJumpInput;
            _inputs.CharacterControls.Jump.canceled += OnJumpInput;
            _inputs.CharacterControls.Jump.performed += OnJumpInput;

            _inputs.CharacterControls.Shoot.started += OnShootInput;
            _inputs.CharacterControls.Shoot.canceled += OnShootInput;
            _inputs.CharacterControls.Shoot.performed += OnShootInput;

            _inputs.CharacterControls.Reload.started += OnReloadInput;
            _inputs.CharacterControls.Reload.canceled += OnReloadInput;
            _inputs.CharacterControls.Reload.performed += OnReloadInput;

            // _inputs.CharacterControls.Aim.started += OnAimInput;
            // _inputs.CharacterControls.Aim.canceled += OnAimInput;
            // _inputs.CharacterControls.Aim.performed += OnAimInput;
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            _rawInput = context.ReadValue<Vector2>();
            _motion = new Vector3(_rawInput.x, 0, _rawInput.y);

            if (_isRunning) {
                _motion = _motion * 2; // _inherentRunSpeed
            }
        }

        private void OnRotationInput(InputAction.CallbackContext context)
        {

        }

        private void OnRunInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (_isRunning == true) {
                    _isRunning = false;
                }
                else {
                    _isRunning = true;
                }

                _inputs.CharacterControls.Move.Reset();
            }
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed) {
                Debug.Log("Jump Triggered: " + context.ReadValueAsButton());
            }
        }

        private void OnShootInput(InputAction.CallbackContext context)
        {
            if (context.performed) {
                Debug.Log("Shoot Triggered: " + context.ReadValueAsButton());
            }
        }

        private void OnReloadInput(InputAction.CallbackContext context)
        {
            if (context.performed) {
                Debug.Log("Reload Triggered: " + context.ReadValueAsButton());
            }
        }

        // private void OnAimInput(InputAction.CallbackContext context)
        // {
        //    if (context.performed) {
        //        Debug.Log("Aim Triggered: " + context.ReadValueAsButton());
        //    }
        // }

        private void handleRotation()
        {
            Vector3 positionToLookAt = new Vector3(_motion.x, 0.0f, _motion.z);
            Quaternion currentRotationPos = transform.rotation;
            Quaternion targetRotationPos;
            Quaternion smoothRotation;

            if (positionToLookAt != Vector3.zero) // If view angle do not differ from last frame
            {
                currentRotationPos = transform.rotation;
                targetRotationPos = Quaternion.LookRotation(positionToLookAt); // Produce log message when positionToLookAt == (0,0,0)
                smoothRotation = Quaternion.Slerp(currentRotationPos, targetRotationPos, _inherentRotationFactor * Time.deltaTime);

                transform.rotation = smoothRotation;
                _camera.FollowPlayerDirection(smoothRotation);
            }
        }

        private void handleAnimation()
        {
            bool isDead = _animator.GetBool("isDead");
            bool isShooting = _animator.GetBool("isShooting");
            float speed = _animator.GetFloat("speed");

            _animator.SetFloat("speed", _motion.magnitude);
        }
    }
}
