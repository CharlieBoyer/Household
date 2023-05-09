using UnityEngine;
using UnityEngine.AI;

namespace Foes
{
    public class FoeController : MonoBehaviour
    {
        private NavMeshAgent _navAgent;
        private Transform _target;
        
        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _target = GameObject.Find("Home").transform;
        }

        private void Start()
        {
            _navAgent.destination = _target.position;
            _navAgent.speed = GetComponent<Foe>().movementSpeed;
        }
    }
}
