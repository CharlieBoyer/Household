using UnityEngine;
using UnityEngine.AI;

namespace Foes
{
    public class FoeController : MonoBehaviour
    {
        [Header("Movement Randomness")]
        public float movementRange = 2f; // The range of random movement
        public float movementInterval = 2f; // The interval at which to change direction
        public float rotationSpeed = 2f; // The speed at which to rotate toward the target

        private NavMeshAgent _navAgent;
        private GameObject _target;

        private Vector3 _originalPosition;
        private float _movementTimer;

        private void Start()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _originalPosition = transform.position;
            _target = GameObject.Find("Home");
        }

        private void Update()
        {
            if (_navAgent.enabled)
            {
                if (_navAgent.remainingDistance < _navAgent.stoppingDistance)
                {
                    _movementTimer += Time.deltaTime;
                    if (_movementTimer >= movementInterval)
                    {
                        // Generate a random target within the movement range
                        Vector3 randomPosition = _originalPosition + Random.insideUnitSphere * movementRange;
                        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, movementRange, NavMesh.AllAreas))
                        {
                            _navAgent.SetDestination(hit.position);
                            _movementTimer = 0f;
                        }
                    }
                }

                // Rotate toward the target
                Quaternion targetRotation = Quaternion.LookRotation(_navAgent.steeringTarget - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Set the home object as the NavMeshAgent's target
            if (_navAgent.destination != _target.transform.position)
            {
                _navAgent.SetDestination(_target.transform.position);
            }
        }
    }
}
