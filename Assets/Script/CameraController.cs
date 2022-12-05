using UnityEngine;
using Cinemachine;
using UnityEditor;

namespace Script
{
    public class CameraController : MonoBehaviour
    {
        public bool cursorLock = true;
        public float sensitivityX = 1f;
        public float sensitivityY = 1f;

        [SerializeField] private float InnateSensitivity = 100f;

        private CinemachineBrain _cameraBrain;
        public CinemachineVirtualCamera _activeCamera;
        static CinemachineVirtualCamera[] _vCameraList;

        private void Start()
        {
            LockCursor();
            SetupCameras();
        }

        private void SetupCameras()
        {
            _cameraBrain = CinemachineCore.Instance.GetActiveBrain(0);
            _activeCamera = _cameraBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
            _vCameraList = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        }

        private void LockCursor()
        {
            if (cursorLock == true) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void FollowPlayerDirection(Quaternion targetRotation)
        {
            _activeCamera.transform.rotation = targetRotation;
        }
    }
}
