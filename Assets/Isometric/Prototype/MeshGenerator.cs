using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private MeshFilter _meshFilter;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();

        _meshFilter.mesh = CreateMesh();
    }

    private Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.name = "Terrain Mesh";

        mesh.vertices = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 1f), };
        mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
        mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
        mesh.uv = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f) };

        return mesh;
    }
}
