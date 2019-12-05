using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal class RangeWeaponCursor : PlayerCursor
    {
        [SerializeField]
        private Transform cursorTransform;

        [SerializeField]
        private Transform rayGuideTransform;

        public override void CursorUpdate()
        {
            cursorTransform.localPosition = Input.mousePosition;
        }

        public void SetRayGuide(Vector3 origin, Vector3 hit)
        {
            rayGuideTransform.localPosition = hit;
        }
    }
}