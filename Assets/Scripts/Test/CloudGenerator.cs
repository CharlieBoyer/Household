using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable]
public class CloudGenerator
{
    [Header("Clouds properties")]
    public float radius = 10f;
    public float height = 5f;
    public float noiseFrequency = 0.01f;
    public float noiseAmplitude = 0.5f;
    public int meshResolution;
    public Color color;
    
    // Cached property
    private static readonly int Surface = Shader.PropertyToID("_Surface");

    public GameObject GenerateClouds()
    {
        UniversalRenderPipelineAsset pipelineAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;

        GameObject cloudObject = new GameObject("Cloud");
        MeshFilter meshFilter = cloudObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cloudObject.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = cloudObject.AddComponent<MeshCollider>();

        // cloudObject.transform.parent = GameObject.Find("Environment").transform;

        meshRenderer.material = (pipelineAsset != null)
            ? pipelineAsset.defaultMaterial
            : throw new Exception("Invalid URP Asset; Could not assign a default material");

        Mesh mesh = new Mesh
        {
            name = "ProceduralCloud",
            vertices = GenerateVertices(meshResolution, radius, height),
            triangles = GenerateTriangles(meshResolution) // Generate the faces of the cloud's mesh
        };
        mesh.Optimize();
        
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        meshRenderer.material.color = color;
        meshRenderer.material.SetFloat(Surface, 0.5f);

        return cloudObject;
    }

    private Vector3[] GenerateVertices(int resolution, float cloudRadius, float cloudHeight)
    {
        Vector3[] vertices = new Vector3[resolution * resolution]; // Generate the "3D-Grid" of vertices making the mesh
        float deltaAngle = 2f * Mathf.PI / (resolution - 1); // Evenly distribute all vertices
        
        for (int i = 0; i < resolution; i++) // Grid of rows and columns
        {
            for (int j = 0; j < resolution; j++)
            {
                float angle = j * deltaAngle;
                float x = cloudRadius * Mathf.Cos(angle); //* Spherical distribution *//
                float z = cloudRadius * Mathf.Sin(angle); //* Spherical distribution *//
                float y = Mathf.PerlinNoise(x * noiseFrequency, z * noiseFrequency) * noiseAmplitude;
                y += cloudHeight; // add the cloudHeight offset
                vertices[i * resolution + j] = new Vector3(x, y, z);
            }
        }

        if (vertices == null)
            throw new Exception("Vertices array empty");
        
        Debug.Log("Vertices count : " + vertices.Length);
        Debug.Log("Vertices obj : " + vertices);
        
        return vertices;
    }

    private int[] GenerateTriangles(int resolution)
    {
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triangleIndex = 0;
        for (int i = 0; i < resolution - 1; i++)
        {
            for (int j = 0; j < resolution - 1; j++)
            {
                int vertexIndex = i * resolution + j;
                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + 1;
                triangles[triangleIndex++] = vertexIndex + resolution;

                triangles[triangleIndex++] = vertexIndex + 1;
                triangles[triangleIndex++] = vertexIndex + resolution + 1;
                triangles[triangleIndex++] = vertexIndex + resolution;
            }
        }
        return triangles;
    }
}
