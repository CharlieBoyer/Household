using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class CameraController : MonoBehaviour
    {
        public float sensitivityX = 1f;
        public float sensitivityY = 1f;
        const float InnateSensitivity = 100f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }
}
