using UnityEngine;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _inherentSpeed;
        [SerializeField] private float _inherentRunSpeed;
        // [SerializeField] private float _inherentRotationFactor;
        // [SerializeField] private float _inherentJumpForce;
        // [SerializeField] private float _gravity;

        private Animator _animator;
        private CharacterController _character;
        private CameraController _camera;

        private Vector3 _motionForward;
        private Vector3 _motionLateral;
        private Vector3 _targetAngle;
        private Quaternion _targetRotation;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterController>();
            _camera = GameObject.Find("MainCamera").GetComponent<CameraController>();
        }

        private void Update()
        {
            /* Delegate movement to Player class */
            // HandleRotation();
            // HandleMovement();
            // HandleAnimation();
        }

        public void HandleMovement()
        {
            float inputForward = Input.GetAxis("Vertical") * _inherentSpeed;
            float inputLateral = Input.GetAxis("Horizontal") * _inherentSpeed;

            if (Input.GetKey(KeyCode.LeftShift)) {
                inputForward *= _inherentRunSpeed;
                inputLateral *= _inherentRunSpeed;
            }

            _motionForward = transform.forward * inputForward;
            _motionLateral = transform.right * inputLateral;

            _character.Move(_motionForward * Time.deltaTime);
            _character.Move(_motionLateral * Time.deltaTime);
        }

        public void HandleRotation()
        {
            float inputX = Input.GetAxis("Mouse X") * _camera.inherentSensitivity * _camera.sensitivityX * Time.deltaTime;

            _targetAngle += new Vector3(0, inputX, 0);
            _targetRotation.eulerAngles = _targetAngle;
            transform.rotation = _targetRotation;
        }

        public void HandleAnimation()
        {
            _animator.SetFloat("Speed", _motionForward.magnitude);
            // _animator.SetFloat("Jump", _jumpVector.magnitude);
        }

        /* public void HandleJump()
        {
            if (_character.isGrounded) {
                Debug.Log("isGrounded : " + _character.isGrounded);
                _motionForward.y = 0;

                if (Input.GetButtonDown("Jump")) {
                    _motionForward.y = _inherentJumpForce;
                }
            }
            else {
                _motionForward.y -= _gravity * Time.deltaTime;
            }
        } */

        /* private void Shoot()
        {
            Debug.Log("Shoot Triggered");
        } */

        /* private void Reload()
        {
            Debug.Log("Reload Triggered");
        } */

        /* private void Turn()
        {

        } */

        /* private void Aim()
        {
          Debug.Log("Aim Triggered");
        } */
    }
}
