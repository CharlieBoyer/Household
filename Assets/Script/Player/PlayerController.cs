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
        // [SerializeField] private float _smoothTime;

        private PlayerInput _inputs;
        private Animator _animator;
        private CharacterController _character;
        private CameraController _camera;

        private Vector2 _rawInput;
        private Vector3 _motion;

        void Awake()
        {
            _inputs = new PlayerInput();
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterController>();
            _camera = GameObject.Find("MainCamera").GetComponent<CameraController>();

            _inputs.CharacterControls.Move.started += OnMovementInput;
            _inputs.CharacterControls.Move.canceled += OnMovementInput;
            _inputs.CharacterControls.Move.performed += OnMovementInput;
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

        void OnMovementInput(InputAction.CallbackContext context)
        {
            _rawInput = context.ReadValue<Vector2>();
            _motion = new Vector3(_rawInput.x, 0, _rawInput.y);
        }

        private void handleRotation()
        {
            Vector3 positionToLookAt = new Vector3(_motion.x, 0.0f, _motion.z);
            Quaternion currentRotationPos = transform.rotation;
            Quaternion targetRotationPos;
            Quaternion smoothRotation;

            if (positionToLookAt != Vector3.zero)
            {
                currentRotationPos = transform.rotation;
                targetRotationPos = Quaternion.LookRotation(positionToLookAt); // Produce log message when positionToLookAt == (0,0,0)
                smoothRotation = Quaternion.Slerp(currentRotationPos, targetRotationPos, _inherentRotationFactor * Time.deltaTime);

                transform.rotation = smoothRotation;
                _camera.Rotate(smoothRotation);
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
