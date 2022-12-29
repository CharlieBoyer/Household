using System;
using UnityEngine;

namespace Script.Weapon
{
    public class IKTargets : MonoBehaviour
    {
        private Animator _controller;
        private GameObject _camera;
        private Transform _shotgun;
        private Transform _leftGripIK;
        private Transform _rightGripIK;
        private float _speed;

        public void Awake()
        {
            _controller = GameObject.Find("Player").GetComponent<Animator>();
            _camera = GameObject.Find("MainCamera");
            _shotgun = this.transform;
            _leftGripIK = _shotgun.GetChild(0);
            _rightGripIK = _shotgun.GetChild(1);
        }

        private void Update()
        {
            IKCorrections();
        }

        private void IKCorrections()
        {
            _speed = _controller.GetFloat("Speed");

            _shotgun.transform.eulerAngles = new Vector3(0, _shotgun.eulerAngles.y, -(_camera.transform.eulerAngles.x));

            if (_speed > 0 && _speed <= 0.2) {
                // _shotgun.localPosition =  Vector3.Lerp(new Vector3(0.163f, 1.42f, 0.267f), _shotgun.localPosition, _speed);
                _leftGripIK.localPosition = new Vector3(0.359f, -0.065f, 0.124f);
                _rightGripIK.localPosition = new Vector3(-0.354f, -0.148f, -0.147f);
                _rightGripIK.localEulerAngles = new Vector3(0.812f, -6.233f, -79.63f);
            }

            if (_speed > 0.2 && _speed <= 1) {
                // _shotgun.localPosition = Vector3.Lerp(_shotgun.localPosition, new Vector3(0.163f, 1.108f, 0.266f), _speed);
                // _leftGripIK.localPosition = new Vector3(0.307f, -0.05f, 0.18f);
                _leftGripIK.localEulerAngles = new Vector3(-101.1f, 225f, -259.8f);
                _rightGripIK.localPosition = new Vector3(-0.4f, -0.23f, -0.107f);
                _rightGripIK.localEulerAngles = new Vector3(2.327f, -5.84f, -65.37f);
            }

            if (_speed > 1 && _speed <= 2)
            {
                _leftGripIK.localPosition = new Vector3(0.359f, -0.065f, 0.182f);
                _rightGripIK.localPosition = new Vector3(-0.282f, -0.21f, -0.116f);
                _rightGripIK.localEulerAngles = new Vector3(2.167f, -5.9f, -66.923f);
            }
        }
    }
}
