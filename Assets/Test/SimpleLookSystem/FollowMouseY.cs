using UnityEngine;

namespace Test
{
    public class FollowMouseY : MonoBehaviour
    {
        public float angleMin;
        public float angleMax;

        Vector3 targetEulerAngle;
        Quaternion targetRotation;

        void Update()
        {
            RotateX();
        }

        void RotateX()
        {
            float currentAngleY = transform.rotation.eulerAngles.y;
            float inputY = Input.GetAxis("Mouse Y");

            targetEulerAngle += new Vector3(-inputY, 0, 0);
            targetEulerAngle.y = currentAngleY; // Keep same angle.y as parent*

            targetEulerAngle.x = Mathf.Clamp(targetEulerAngle.x, -angleMax, -angleMin);

            targetRotation.eulerAngles = targetEulerAngle;
            transform.rotation = targetRotation;
        }
    }
}
