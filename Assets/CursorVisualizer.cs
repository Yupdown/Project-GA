using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisualizer : MonoBehaviour
{
    [SerializeField]
    private Transform _cursorTransform;

    [SerializeField]
    private Transform _rayGuideTransform;

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        _cursorTransform.localPosition = Input.mousePosition;
    }

    public void SetRayGuide(Vector3 origin, Vector3 hit)
    {
        _rayGuideTransform.localPosition = hit;
    }
}
