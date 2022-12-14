using UnityEngine;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _inherentSpeedFactor;
        [SerializeField] private float _inherentRunSpeedFactor;
        [SerializeField] private float _inherentRotationFactor;
        [SerializeField] private float _inherentJumpForce;
        [SerializeField] float _gravity;

        private Animator _animator;
        private CharacterController _character;
        private CameraController _camera;
        private Collider _groundCollider;
        
        private Vector3 _motionVector;
        private Vector3 _jumpVector;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterController>();
            _camera = GameObject.Find("MainCamera").GetComponent<CameraController>();
        }

        public void HandleMovement()
        {
            _motionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            if (_motionVector.x == 0 && _motionVector.z == 0) { // If no motion, do nothing
                return;
            }
            if (Input.GetKey(KeyCode.LeftShift)) {
                _motionVector *= _inherentRunSpeedFactor;
            }
            
            _character.Move(_motionVector * (_inherentSpeedFactor * Time.deltaTime));
        }
        
        public void HandleRotation()
        {
            Vector3 positionToLookAt = new Vector3(_motionVector.x, 0.0f, _motionVector.z);
            Quaternion currentRotationPos;
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
        
        public void HandleAnimation()
        {
            _animator.SetFloat("Speed", _motionVector.magnitude);
            _animator.SetFloat("Jump", _jumpVector.magnitude);
        }

        public void HandleJump()
        {
            if (_character.isGrounded)
            {
                Debug.Log("isGrounded : " + _character.isGrounded);
                _jumpVector = Vector3.zero;

                if (Input.GetButtonDown("Jump")) {
                    _jumpVector = new Vector3(0, _inherentJumpForce, 0);
                }

                _jumpVector.y -= _gravity * Time.deltaTime;
                _character.Move(_jumpVector * Time.deltaTime);
            }
        }

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
