using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class PlayerInterface : MonoBehaviour
    {
        [SerializeField]
        private PlayerCursor[] cursors;
        
        private void Awake()
        {
            ClearCursor();

            SwitchCursor(CursorType.RangeWeapon);
        }

        private void ClearCursor()
        {
            foreach (PlayerCursor cursor in cursors)
            {
                cursor.gameObject.SetActive(false);
            }
        }

        public void SwitchCursor(CursorType cursor)
        {
            ClearCursor();

            cursors[(uint)cursor].gameObject.SetActive(true);
        }
    }

    public enum CursorType
    {
        Default,
        MeleeWeapon,
        RangeWeapon
    }
}