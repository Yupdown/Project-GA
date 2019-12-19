using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal class RangeWeaponCursor : PlayerCursor
    {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private Camera aimingCamera;

        [SerializeField]
        private RectTransform cursorTransform;

        [SerializeField]
        private RectTransform rayGuideTransform;

        [SerializeField]
        private LineRenderer lineRenderer;

        private Ray screenRay;
        private RaycastHit screenRayHit;

        private Ray aimingRay;
        private RaycastHit aimingRayHit;

        public override void CursorUpdate(PlayerWeaponHandler handler)
        {
            Vector2 mousePosition = ScreenToLocalPosition(Input.mousePosition);

            cursorTransform.localPosition = mousePosition;

            screenRay = aimingCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(screenRay, out screenRayHit))
            {
                Vector3 aimingOrigin = playerTransform.localPosition;

                aimingRay = new Ray(aimingOrigin, (screenRayHit.point - aimingOrigin).normalized);

                if (Physics.Raycast(aimingRay, out aimingRayHit))
                {
                    Vector2 targetPosition = WorldToLocalPosition(aimingRayHit.point);

                    rayGuideTransform.localPosition = targetPosition;

                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, Vector2.zero);
                    lineRenderer.SetPosition(1, targetPosition);

                    handler.AimWeapon(aimingRayHit.point);

                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        handler.AttackWithTargetPosition(aimingRayHit.point);
                    }
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(screenRay.origin, screenRayHit.point);
            Gizmos.DrawLine(aimingRay.origin, aimingRayHit.point);
        }
    }
}