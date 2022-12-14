using UnityEngine;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _inherentSpeedFactor;
        [SerializeField] private float _inherentRunSpeedFactor;
        [SerializeField] private float _inherentRotationFactor;
       
        
        private Animator _animator;
        private CharacterController _character;
        private CameraController _camera;
        
        private Vector3 _motion;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterController>();
            _camera = GameObject.Find("MainCamera").GetComponent<CameraController>();
        }

        public void HandleMovement()
        {
            _motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (_motion.x == 0 && _motion.z == 0) { // If no motion, do nothing
                return;
            }
            
            if (Input.GetKey(KeyCode.LeftShift)) {
                _motion *= _inherentRunSpeedFactor;
            }
            
            _character.Move(_motion * (_inherentSpeedFactor * Time.deltaTime));
        }

        public void Jump()
        {
            
        }

        private void Shoot()
        {
            Debug.Log("Shoot Triggered");
        }

        private void Reload()
        {
            Debug.Log("Reload Triggered");
        }
        
        private void Turn()
        {

        }

        // private void Aim()
        // {
        //    if (context.performed) {
        //        Debug.Log("Aim Triggered: " + context.ReadValueAsButton());
        //    }
        // }

        public void HandleRotation()
        {
            Vector3 positionToLookAt = new Vector3(_motion.x, 0.0f, _motion.z);
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
            // bool isDead = _animator.GetBool("isDead");
            // bool isShooting = _animator.GetBool("isShooting");
            // float speed = _animator.GetFloat("speed");

            _animator.SetFloat("speed", _motion.magnitude);
        }
    }
}
