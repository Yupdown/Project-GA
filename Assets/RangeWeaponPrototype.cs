using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponPrototype : MonoBehaviour
{
    [SerializeField]
    private Camera _aimingCamera;

    [SerializeField]
    private CursorVisualizer _cursor;

    [SerializeField]
    private LineRenderer _lineRenderer;

    private Ray _screenRay;
    private RaycastHit _screenRayHit;

    private Ray _aimingRay;
    private RaycastHit _aimingRayHit;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _screenRay = _aimingCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_screenRay, out _screenRayHit))
        {
            Vector3 aimingOrigin = _transform.localPosition;

            _aimingRay = new Ray(aimingOrigin, (_screenRayHit.point - aimingOrigin).normalized);

            if (Physics.Raycast(_aimingRay, out _aimingRayHit))
            {
                _cursor.SetRayGuide(_aimingCamera.WorldToScreenPoint(_aimingRay.origin), _aimingCamera.WorldToScreenPoint(_aimingRayHit.point));

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPositions(new Vector3[] { _aimingRay.origin, _aimingRayHit.point });
                    _lineRenderer.widthMultiplier = 0.2f;
                }
            }
        }

        _lineRenderer.widthMultiplier = Mathf.Lerp(_lineRenderer.widthMultiplier, 0f, Time.deltaTime * 16f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_screenRay.origin, _screenRayHit.point);
        Gizmos.DrawLine(_aimingRay.origin, _aimingRayHit.point);
    }
}
