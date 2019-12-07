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

        private Ray screenRay;
        private RaycastHit screenRayHit;

        private Ray aimingRay;
        private RaycastHit aimingRayHit;

        public override void CursorUpdate(PlayerWeaponHandler handler)
        {
            cursorTransform.localPosition = ScreenToLocalPosition(Input.mousePosition);

            screenRay = aimingCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(screenRay, out screenRayHit))
            {
                Vector3 aimingOrigin = playerTransform.localPosition;

                aimingRay = new Ray(aimingOrigin, (screenRayHit.point - aimingOrigin).normalized);

                if (Physics.Raycast(aimingRay, out aimingRayHit))
                {
                    rayGuideTransform.localPosition = WorldToLocalPosition(aimingRayHit.point);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
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