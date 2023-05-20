using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Test
{
    public class GenerateMeshObject : MonoBehaviour
    {
        public int resolution;
        public float meshSize = 1f;

        void Start()
        {
            GenerateCube(meshSize);
        }

        private void GenerateCube(float size)
        {
            // Create a new game object with a mesh renderer
            GameObject newObject = new GameObject("Cube");
            MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();

            // Generate a new mesh
            Mesh mesh = new Mesh();

            // Define the vertices of the cube
            Vector3[] vertices = new Vector3[8];
            vertices[0] = new Vector3(-meshSize, -meshSize, -meshSize);
            vertices[1] = new Vector3(meshSize, -meshSize, -meshSize);
            vertices[2] = new Vector3(meshSize, -meshSize, meshSize);
            vertices[3] = new Vector3(-meshSize, -meshSize, meshSize);
            vertices[4] = new Vector3(-meshSize, meshSize, -meshSize);
            vertices[5] = new Vector3(meshSize, meshSize, -meshSize);
            vertices[6] = new Vector3(meshSize, meshSize, meshSize);
            vertices[7] = new Vector3(-meshSize, meshSize, meshSize);

            // Define the triangles of each face
            int[] triangles = new int[36];
            // Front face
            triangles[0] = 0; triangles[1] = 4; triangles[2] = 5;
            triangles[3] = 5; triangles[4] = 1; triangles[5] = 0;
            // Right face
            triangles[6] = 1; triangles[7] = 5; triangles[8] = 6;
            triangles[9] = 6; triangles[10] = 2; triangles[11] = 1;
            // Back face
            triangles[12] = 2; triangles[13] = 6; triangles[14] = 7;
            triangles[15] = 7; triangles[16] = 3; triangles[17] = 2;
            // Left face
            triangles[18] = 3; triangles[19] = 7; triangles[20] = 4;
            triangles[21] = 4; triangles[22] = 0; triangles[23] = 3;
            // Top face
            triangles[24] = 4; triangles[25] = 7; triangles[26] = 6;
            triangles[27] = 6; triangles[28] = 5; triangles[29] = 4;
            // Bottom face
            triangles[30] = 3; triangles[31] = 0; triangles[32] = 1;
            triangles[33] = 1; triangles[34] = 2; triangles[35] = 3;

            // Assign the mesh data
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            // Assign the mesh to the object's mesh filter
            MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            
            UniversalRenderPipelineAsset pipelineAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
            meshRenderer.material = (pipelineAsset != null)
                ? pipelineAsset.defaultMaterial
                : throw new Exception("GenerateMeshObject > Invalid URP Asset");
        }
    }
}
