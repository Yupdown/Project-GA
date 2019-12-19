using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void CreateAndApplyMesh(float[,] heightMap)
    {
        Mesh meshInstance = CreateMesh(heightMap);

        _meshFilter.mesh = meshInstance;
        _meshCollider.sharedMesh = meshInstance;
    }

    private Mesh CreateMesh(float[,] heightMap)
    {
        Mesh mesh = new Mesh();

        mesh.name = "Terrain Mesh";

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        int mapWidth = heightMap.GetLength(0);
        int mapHeight = heightMap.GetLength(1);

        for (int x = 0, triCount = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                float height = heightMap[x, z];

                vertices.Add(new Vector3(x, height, z));
                vertices.Add(new Vector3(x + 1f, height, z));
                vertices.Add(new Vector3(x, height, z + 1f));
                vertices.Add(new Vector3(x + 1f, height, z + 1f));

                int triangleIndex = triCount++ * 4;
                triangles.Add(triangleIndex);
                triangles.Add(triangleIndex + 2);
                triangles.Add(triangleIndex + 1);
                triangles.Add(triangleIndex + 2);
                triangles.Add(triangleIndex + 3);
                triangles.Add(triangleIndex + 1);

                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);
                normals.Add(Vector3.up);

                uvs.Add(new Vector2(0f, 0f));
                uvs.Add(new Vector2(1f, 0f));
                uvs.Add(new Vector2(0f, 1f));
                uvs.Add(new Vector2(1f, 1f));

                float helghtDelta = z > 0 ? heightMap[x, z - 1] : 0f;

                if (helghtDelta < height)
                {
                    vertices.Add(new Vector3(x, helghtDelta, z));
                    vertices.Add(new Vector3(x + 1f, helghtDelta, z));
                    vertices.Add(new Vector3(x, height, z));
                    vertices.Add(new Vector3(x + 1f, height, z));
                    
                    triangleIndex = triCount++ * 4;
                    triangles.Add(triangleIndex);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 1);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 3);
                    triangles.Add(triangleIndex + 1);
                    
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    normals.Add(Vector3.back);
                    
                    uvs.Add(new Vector2(0f, 0f));
                    uvs.Add(new Vector2(1f, 0f));
                    uvs.Add(new Vector2(0f, 1f));
                    uvs.Add(new Vector2(1f, 1f));
                }

                helghtDelta = (z < mapHeight - 1) ? heightMap[x, z + 1] : 0f;

                if (helghtDelta < height)
                {
                    vertices.Add(new Vector3(x + 1f, helghtDelta, z + 1f));
                    vertices.Add(new Vector3(x, helghtDelta, z + 1f));
                    vertices.Add(new Vector3(x + 1f, height, z + 1f));
                    vertices.Add(new Vector3(x, height, z + 1f));

                    triangleIndex = triCount++ * 4;
                    triangles.Add(triangleIndex);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 1);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 3);
                    triangles.Add(triangleIndex + 1);

                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);
                    normals.Add(Vector3.forward);

                    uvs.Add(new Vector2(0f, 0f));
                    uvs.Add(new Vector2(1f, 0f));
                    uvs.Add(new Vector2(0f, 1f));
                    uvs.Add(new Vector2(1f, 1f));
                }

                helghtDelta = x > 0 ? heightMap[x - 1, z] : 0f;

                if (helghtDelta < height)
                {
                    vertices.Add(new Vector3(x, helghtDelta, z + 1f));
                    vertices.Add(new Vector3(x, helghtDelta, z));
                    vertices.Add(new Vector3(x, height, z + 1f));
                    vertices.Add(new Vector3(x, height, z));

                    triangleIndex = triCount++ * 4;
                    triangles.Add(triangleIndex);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 1);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 3);
                    triangles.Add(triangleIndex + 1);

                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);
                    normals.Add(Vector3.left);

                    uvs.Add(new Vector2(0f, 0f));
                    uvs.Add(new Vector2(1f, 0f));
                    uvs.Add(new Vector2(0f, 1f));
                    uvs.Add(new Vector2(1f, 1f));
                }

                helghtDelta = (x < mapWidth - 1) ? heightMap[x + 1, z] : 0f;

                if (helghtDelta < height)
                {
                    vertices.Add(new Vector3(x + 1f, helghtDelta, z));
                    vertices.Add(new Vector3(x + 1f, helghtDelta, z + 1f));
                    vertices.Add(new Vector3(x + 1f, height, z));
                    vertices.Add(new Vector3(x + 1f, height, z + 1f));

                    triangleIndex = triCount++ * 4;
                    triangles.Add(triangleIndex);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 1);
                    triangles.Add(triangleIndex + 2);
                    triangles.Add(triangleIndex + 3);
                    triangles.Add(triangleIndex + 1);

                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);
                    normals.Add(Vector3.right);

                    uvs.Add(new Vector2(0f, 0f));
                    uvs.Add(new Vector2(1f, 0f));
                    uvs.Add(new Vector2(0f, 1f));
                    uvs.Add(new Vector2(1f, 1f));
                }
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);

        return mesh;
    }
}
