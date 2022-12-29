using UnityEngine;

namespace Test
{
    public class SmoothRotation : MonoBehaviour
    {
        public Transform from;
        public Transform to;

        public float timeCount = 0.0f;

        void FixedUpdate()
        {
            transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
            timeCount = timeCount + Time.deltaTime;

            if (transform.rotation == to.rotation) {
                timeCount = 0.0f;
            }
        }
    }
}
