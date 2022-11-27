using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class FoesController : MonoBehaviour
    {
        public Color prototypeColor = Color.red;
        private MeshRenderer _prototypeMeshRenderer;

        void Awake()
        {
            _prototypeMeshRenderer = GetComponent<MeshRenderer>();
            _prototypeMeshRenderer.material.color = prototypeColor;
        }

        void Update()
        {
        
        }
    }
}

