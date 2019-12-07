using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal class MeleeWeaponCursor : PlayerCursor
    {
        [SerializeField]
        private RectTransform cursorTransform;

        public override void CursorUpdate(PlayerWeaponHandler handler)
        {
            cursorTransform.localPosition = ScreenToLocalPosition(Input.mousePosition);

            Ray screenRay = rootCanvas.worldCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit screenRayHit;

            if (Physics.Raycast(screenRay, out screenRayHit))
            {
                handler.AimWeapon(screenRayHit.point);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    handler.AttackWithTargetPosition(screenRayHit.point);
                }
            }
        }
    }
}