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
        [SerializeField] private float _inherentRunSpeed;

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

            RegisterInputsCallbacks(_inputs);
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

            if (_isRunning) {
                _inherentSpeedFactor = _inherentSpeedFactor * _inherentRunSpeed;
            }
            _character.Move(_motion * _inherentSpeedFactor * Time.deltaTime);
        }

        private void RegisterInputsCallbacks(PlayerInput inputs)
        {
            inputs.CharacterControls.Move.started += OnMovementInput;
            inputs.CharacterControls.Move.canceled += OnMovementInput;
            inputs.CharacterControls.Move.performed += OnMovementInput;

            inputs.CharacterControls.Rotation.started += OnRotationInput;
            inputs.CharacterControls.Rotation.canceled += OnRotationInput;
            inputs.CharacterControls.Rotation.performed += OnRotationInput;

            inputs.CharacterControls.Run.started += OnRunInput;
            inputs.CharacterControls.Run.canceled += OnRunInput;
            inputs.CharacterControls.Run.performed += OnRunInput;

            inputs.CharacterControls.Jump.started += OnJumpInput;
            inputs.CharacterControls.Jump.canceled += OnJumpInput;
            inputs.CharacterControls.Jump.performed += OnJumpInput;

            inputs.CharacterControls.Shoot.started += OnShootInput;
            inputs.CharacterControls.Shoot.canceled += OnShootInput;
            inputs.CharacterControls.Shoot.performed += OnShootInput;

            inputs.CharacterControls.Reload.started += OnReloadInput;
            inputs.CharacterControls.Reload.canceled += OnReloadInput;
            inputs.CharacterControls.Reload.performed += OnReloadInput;

            // _inputs.CharacterControls.Aim.started += OnAimInput;
            // _inputs.CharacterControls.Aim.canceled += OnAimInput;
            // _inputs.CharacterControls.Aim.performed += OnAimInput;
        }

        private void OnMovementInput(InputAction.CallbackContext context)
        {
            _rawInput = context.ReadValue<Vector2>();
            _motion = new Vector3(_rawInput.x, 0, _rawInput.y);
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

                // _inputs.CharacterControls.Move.Reset();
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
