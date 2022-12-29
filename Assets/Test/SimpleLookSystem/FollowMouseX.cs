using UnityEngine;

namespace Test
{
    public class FollowMouseX : MonoBehaviour
    {
        public float FOVMin;
        public float FOVMax;

        Vector3 currentEulerAngle;
        Quaternion currentRotation;

        void Update()
        {
            RotateY();
        }

        void RotateY()
        {
            float inputX = Input.GetAxis("Mouse X");

            currentEulerAngle += new Vector3(0, inputX, 0);

            this.transform.rotation = currentRotation;
            currentRotation.eulerAngles = currentEulerAngle;
        }
    }
}
