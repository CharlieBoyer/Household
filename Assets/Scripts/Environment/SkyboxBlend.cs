using UnityEngine;
using UnityEngine.Rendering;

namespace Environment
{
    public class SkyboxBlend : MonoBehaviour
    {
        public Material skyboxA;
        public Material skyboxB;
        
        [Range(0, 1)] public float blendAmount = 0f;

        private void Start()
        {
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        }

        private void OnDestroy()
        {
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        }

        private void OnBeginCameraRendering(ScriptableRenderContext context, Camera sceneCamera)
        {
            if (sceneCamera.cameraType == CameraType.SceneView)
                return;

            if (skyboxA == null || skyboxB == null)
                return;

            RenderSettings.skybox = GetBlendedSkybox();
        }

        private Material GetBlendedSkybox()
        {
            Material material = new Material(skyboxA);;
            
            try
            {
                material = new Material(RenderSettings.skybox);
            }
            catch
            {
                material.CopyPropertiesFromMaterial(RenderSettings.skybox);
            }
            
            material.CopyPropertiesFromMaterial(RenderSettings.skybox);
            material.Lerp(skyboxA, skyboxB, blendAmount);
            return material;
        }

        public void UpdateBlendAmount(float newBlendAmount)
        {
            blendAmount = newBlendAmount;
        }
    }
}
