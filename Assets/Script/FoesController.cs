using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    public class FoesController : MonoBehaviour
    {
        public Transform navGoal;
        private NavMeshAgent _navAgent;

        [SerializeField] private int _speed;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _navAgent.speed = _speed;
            _navAgent.destination = navGoal.position;
        }
    }
}
