using System;
using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    public class FoesController : MonoBehaviour
    {
        public Transform navGoal;
        private NavMeshAgent _navAgent;

        public int speed;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _navAgent.speed = speed;
            _navAgent.destination = GameObject.Find("Home").transform.position;
        }

    }
}
