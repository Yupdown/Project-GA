using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _terrainObject;

    private Transform _transform;

    private const int Width = 64;
    private const int Height = 64;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Start ()
    {
		for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                float height = Mathf.Max(Mathf.Floor(Mathf.PerlinNoise(x * 0.05f, z * 0.05f) * 16f - 8f), 1f);

                Vector3 tilePosition = new Vector3(x - (Width * 0.5f - 0.5f), height * 0.5f, z - (Height * 0.5f - 0.5f));

                Transform terrainTransform = Instantiate(_terrainObject, tilePosition, Quaternion.identity).GetComponent<Transform>();

                terrainTransform.localScale = new Vector3(1f, height, 1f);
            }
        }
	}
}
