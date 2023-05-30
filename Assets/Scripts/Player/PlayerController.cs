using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Base multipliers")]
        [SerializeField] private float inherentSpeed;

        [Header("Speed modifiers")]
        public float speed;
        public float runSpeed;

        [Header("Gravity simulation")]
        [SerializeField] private bool gravityOn;
        public float gravityForce;

        private Transform _body;
        private CharacterController _controller;

        private Vector3 _motion;
        private Vector3 _angularMotion;
        private Quaternion _targetRotation;

        void Awake()
        {
            _body = this.transform;
            _controller = GetComponent<CharacterController>();
        }

        public void Move()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            float inputForward = Input.GetAxis("Vertical") * inherentSpeed * speed * Time.deltaTime;
            float inputLateral = Input.GetAxis("Horizontal") * inherentSpeed * speed * Time.deltaTime;
            float gravityPull = gravityForce * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift)) {
                inputForward *= runSpeed;
                inputLateral *= runSpeed;
            }

            _motion = (_body.forward * inputForward) + (_body.right * inputLateral);
            if (gravityOn)
                _motion += (_body.up * (-1 * gravityPull));
            
            _controller.Move(_motion * Time.deltaTime);
        }

        private void HandleRotation()
        {
            float inputX = Input.GetAxis("Mouse X") * Time.deltaTime;
            inputX *= CameraController.Instance.inherentSensitivity * CameraController.Instance.sensitivityX;
            
            _angularMotion += new Vector3(0, inputX, 0);
            _targetRotation.eulerAngles = _angularMotion;
            transform.rotation = _targetRotation;
        }
    }
}
