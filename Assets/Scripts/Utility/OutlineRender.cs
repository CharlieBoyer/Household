using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [ExecuteInEditMode]
    public class OutlineRender : MonoBehaviour // TODO : DEV ONLY
    {
        public bool enable = false;
        
        [Header("Objects to Outline")]
        public bool renderAllColliders = false;
        public List<GameObject> renderList;

        private void Awake()
        {
            if (!renderAllColliders) return;
            
            renderList = new List<GameObject>(FindObjectsOfType<Collider>().Select(c => c.gameObject).ToArray());
            UnityEditor.SceneView.RepaintAll();
        }

        private void OnDrawGizmos()
        {
            if (!enable)
                return;
            Gizmos.color = Color.yellow;

            foreach (GameObject obj in renderList)
            {
                Collider componentCollider = obj.GetComponent<Collider>();
                if (componentCollider != null)
                {
                    Bounds bounds = componentCollider.bounds;
                    Gizmos.DrawWireCube(bounds.center, bounds.size);
                }
                else
                {
                    Renderer componentRenderer = obj.GetComponent<Renderer>();
                    if (componentRenderer != null)
                    {
                        Bounds bounds = componentRenderer.bounds;
                        Gizmos.DrawWireCube(bounds.center, bounds.size);
                    }
                }
            }
        }
    }
}
