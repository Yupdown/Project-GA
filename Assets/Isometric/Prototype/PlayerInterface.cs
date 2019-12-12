using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gnome.Isometric.Prototype
{
    internal class PlayerInterface : MonoBehaviour
    {
        [SerializeField]
        private PlayerCursor[] cursors;

        private PlayerCursor currentCursor;

        [SerializeField]
        private Text weaponNameText;
        [SerializeField]
        private Image weaponImage;

        private void Awake()
        {
            ClearCursor();
        }

        private void ClearCursor()
        {
            foreach (PlayerCursor cursor in cursors)
            {
                cursor.gameObject.SetActive(false);
            }
        }

        public void UpdateCursor(PlayerWeaponHandler handler)
        {
            if (currentCursor != null)
                currentCursor.CursorUpdate(handler);
        }

        public void SwitchCursor(CursorType cursor)
        {
            ClearCursor();

            currentCursor = cursors[(uint)cursor];
            
            currentCursor.gameObject.SetActive(true);
        }

        public void UpdateWeapon(Weapon targetWeapon)
        {
            weaponNameText.text = targetWeapon.WeaponName;
            weaponImage.sprite = targetWeapon.WeaponTexture;
        }
    }

    public enum CursorType
    {
        Default,
        MeleeWeapon,
        RangeWeapon
    }
}