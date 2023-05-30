using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Foes
{
    public class FoeController : MonoBehaviour
    {
        [Header("NavAgent Control Settings")]
        public int stopDistance;
        public bool enableAutoBrake;
        public float navMeshEdgeDetectionRadius;

        private Foe _body;
        private NavMeshAgent _navAgent;
        private Transform _target;

        private float _gravityForce;

        private void Awake()
        {
            _body = GetComponentInChildren<Foe>();
            _target = GameObject.Find("Home").transform;

            _navAgent = GetComponent<NavMeshAgent>();
            _navAgent.stoppingDistance = stopDistance;
            _navAgent.autoBraking = enableAutoBrake;
            _navAgent.speed = _body.movementSpeed;
            
            StartAgent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Home"))
                StopAgent();
            
            _body.CollisionTrigger(other);
        }

        public void StartAgent()
        {
            Vector3 closestPointToHome = GetClosestPointToHome();

            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(closestPointToHome, out navMeshHit, navMeshEdgeDetectionRadius, NavMesh.AllAreas))
            {
                _navAgent.destination = navMeshHit.position;
            }
        }

        private void StopAgent()
        {
            _navAgent.isStopped = true;
            _navAgent.velocity = Vector3.zero;
        }

        private Vector3 GetClosestPointToHome()
        {
            Vector3 currentPosition = transform.position;
            Vector3 homePosition = _target.position;
            Vector3 rayDirection = homePosition - currentPosition;

            RaycastHit hit;
            if (Physics.Raycast(currentPosition, rayDirection, out hit, Mathf.Infinity))
            {
                // Draw the raycast in the Unity Editor
                Debug.DrawRay(currentPosition, rayDirection, Color.red);
                return hit.point;
            }

            // Draw a ray extending to infinity if no hit is detected
            Debug.DrawRay(currentPosition, rayDirection, Color.blue);

            return currentPosition;
        }
    }
}
