using UnityEngine;
using Internal;

namespace Player
{
    public class CameraController : MonoBehaviourSingleton<CameraController>
    {
        [Header("[DEV ONLY]")] // TODO: [DEV ONLY]
        public bool cursorLock = true;
        public bool cursorVisible = false;
        
        [Header("Sensitivity settings")]
        public float inherentSensitivity;
        public float sensitivityY;
        public float sensitivityX;

        [Header("Vertical constraint")]
        public bool invertYAxis;
        [Range(0, 90f)] public float maxAngle;
        [Range(0, -90f)] public float minAngle;

        private Vector3 _angularMotion;
        private Quaternion _targetRotation;

        private void Start() {
            LockCursor();
        }

        private void Update()
        {
            float inputY = Input.GetAxis("Mouse Y") * inherentSensitivity * sensitivityY * Time.deltaTime;
            float currentAngleY = transform.rotation.eulerAngles.y;

            if (invertYAxis)
                inputY = -inputY;

            _angularMotion += new Vector3(inputY, 0, 0);
            _angularMotion.y = currentAngleY; // Keep same rotation as parent on Y;
            _angularMotion.x = Mathf.Clamp(_angularMotion.x, -maxAngle, -minAngle); // Invert values for correction rotation

            _targetRotation.eulerAngles = _angularMotion;
            transform.rotation = _targetRotation;
        }

        private void LockCursor()
        {
            if (!cursorLock) return;
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = cursorVisible;
        }
    }
}
