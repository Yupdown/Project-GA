using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal abstract class PlayerCursor : MonoBehaviour
    {
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
            CursorUpdate();
        }

        public abstract void CursorUpdate();
    }
}