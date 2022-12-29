using UnityEngine;

namespace Script
{
    public class Home : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.color = Color.cyan;
        }
    }
}
