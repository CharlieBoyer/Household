using UnityEngine;

namespace Script
{
    public class FoesController : MonoBehaviour
    {
        private BaseColor _prototypeColor;
        private MeshRenderer _mesh;

        private void Awake()
        {
            _prototypeColor = GetComponent<BaseColor>();
            _mesh = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _mesh.material.color = _prototypeColor.baseColor;
        }
    }
}
