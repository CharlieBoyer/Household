using System;
using UnityEngine;
using UnityEngine.AI;

namespace Foes
{
    public class FoeController : MonoBehaviour
    {
        [Header("NavAgent Control Settings")] public int stopDistance;
        public bool enableAutoBrake;

        private NavMeshAgent _navAgent;
        private Transform _target;

        private void Awake()
        {
            _target = GameObject.Find("Home").transform;

            _navAgent = GetComponent<NavMeshAgent>();
            _navAgent.stoppingDistance = stopDistance;
            _navAgent.autoBraking = enableAutoBrake;
            _navAgent.speed = GetComponentInChildren<Foe>().movementSpeed;
        }

        public void StartAI()
        {
            SetDestinationToNearestBound();
        }

        private void SetDestinationToNearestBound()
        {
            if (_target == null)
            {
                Debug.LogError("FoeController > Target is not set for FoeController!");
                return;
            }

            Vector3 targetPosition = _target.position;
            NavMeshHit hit;
            if (NavMesh.Raycast(targetPosition, transform.position, out hit, NavMesh.AllAreas))
            {
                _navAgent.SetDestination(hit.position);
            }
            else
            {
                Debug.LogError("FoeController > Failed to find valid destination on NavMesh!");

                // Draw the raycast for debugging purposes
                Debug.DrawRay(targetPosition, transform.position - targetPosition, Color.red);
            }
        }
    }
}
