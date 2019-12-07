using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal abstract class PlayerCursor : MonoBehaviour
    {
        protected Canvas rootCanvas;

        protected RectTransform rectTransform;

        private void Awake()
        {
            rootCanvas = GetComponentInParent<Canvas>();

            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
        }

        public Vector2 WorldToLocalPosition(Vector3 worldPosition)
        {
            return ScreenToLocalPosition(rootCanvas.worldCamera.WorldToScreenPoint(worldPosition));
        }

        public Vector2 ScreenToLocalPosition(Vector3 screenPosition)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, rootCanvas.worldCamera, out localPoint);

            return localPoint;
        }

        public abstract void CursorUpdate(PlayerWeaponHandler handler);
    }
}