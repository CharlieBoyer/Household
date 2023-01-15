using UnityEngine;

namespace Test.Tmp
{
    public class MobMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector2 _motion;
        public float direction = 1f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

        }

        private void Update()
        {
            _motion = new Vector2(direction, 0) * Time.deltaTime;

            _rigidbody.AddForce(_motion, ForceMode.Impulse);
        }
    }
}
