using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerController _controller;
        
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }

        private void Start() {
            Input.ResetInputAxes();
        }

        private void Update()
        {
            _controller.Move();
        }
    }
}
