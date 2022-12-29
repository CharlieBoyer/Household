using UnityEngine;
// using Cinemachine;

namespace Script
{
    public class CameraController : MonoBehaviour
    {
        // Cinemachine
        // private CinemachineBrain _cameraBrain;
        // private CinemachineVirtualCamera[] _vCameraList;
        // private CinemachineVirtualCamera _activeCamera;
        // private GameObject _cameraAnchor;

        public bool cursorLock = true;
        public float inherentSensitivity;
        public float sensitivityY;
        public float sensitivityX;

        [Header("Vertical constraint")]
        [Range(0, 90f)] public float maxAngle;
        [Range(0, -90f)] public float minAngle;

        private Vector3 _targetAngle;
        private Quaternion _targetRotation;

        private void Start() {
            LockCursor();
        }

        private void Update()
        {
            float inputY = Input.GetAxis("Mouse Y") * inherentSensitivity * sensitivityY * Time.deltaTime;
            float currentAngleY = transform.rotation.eulerAngles.y;

            _targetAngle += new Vector3(-inputY, 0, 0); // Keep same rotation as parent on Y;
            _targetAngle.y = currentAngleY;
            _targetAngle.x = Mathf.Clamp(_targetAngle.x, -maxAngle, -minAngle); // Invert values for correction rotation

            _targetRotation.eulerAngles = _targetAngle;
            transform.rotation = _targetRotation;
        }

        private void LockCursor()
        {
            if (cursorLock == true) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        /* private void SetupCameras() {
            _cameraBrain = CinemachineCore.Instance.GetActiveBrain(0);
            _activeCamera = _cameraBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
            _vCameraList = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        } */

        /* public void FollowPlayerDirection(Quaternion playerRotation)
        {
            _activeCamera.transform.rotation = playerRotation;
        } */
    }
}
