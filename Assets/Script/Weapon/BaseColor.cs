using System;
using UnityEngine;

namespace Script
{
    public class BaseColor: MonoBehaviour
    {
        public Color baseColor;

        public MeshRenderer mesh;

        private void Awake()
        {
            mesh = this.GetComponent<MeshRenderer>();
        }
    }
}
