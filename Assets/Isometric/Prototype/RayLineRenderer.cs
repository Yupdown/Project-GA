using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        StartCoroutine(RayLineRendererCoroutine());
    }

    private IEnumerator RayLineRendererCoroutine()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.2f)
        {
            lineRenderer.widthMultiplier = 0.2f * Mathf.Pow(1f - t, 2f);
            yield return null;
        }

        Destroy(gameObject);
    }
}
