using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    internal class PlayerWeaponHandler : MonoBehaviour
    {
        private Weapon[] registeredWeapons;

        private Weapon handledWeapon;

        [SerializeField]
        private WeaponLoader weaponLoader;

        [SerializeField]
        private PlayerInterface playerInterface;

        [SerializeField]
        private Transform weaponTransform;

        [SerializeField]
        private Transform weaponRendererTransform;

        [SerializeField]
        private SpriteRenderer weaponRenderer;

        private Transform cachedTransform;

        private float scrollAmount;

        private void Awake()
        {
            registeredWeapons = weaponLoader.LoadWeapons();

            cachedTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            SwitchWeapon(registeredWeapons[0]);
        }

        private void Update()
        {
            scrollAmount += Input.mouseScrollDelta.y;

            int index = Mathf.Min((int)Mathf.Repeat(scrollAmount, registeredWeapons.Length), registeredWeapons.Length - 1);

            Weapon weapon = registeredWeapons[index];
            if (weapon != handledWeapon)
                SwitchWeapon(weapon);

            playerInterface.UpdateCursor(this);
        }

        public void SwitchWeapon(Weapon weapon)
        {
            handledWeapon = weapon;
            weaponRenderer.sprite = weapon.WeaponTexture;

            playerInterface.SwitchCursor(weapon.AttackType.WeaponCursor);
        }

        public void AimWeapon(Vector3 position)
        {
            Vector3 delta = position - cachedTransform.position;
            float radian = Mathf.Atan2(delta.z, delta.x);

            weaponTransform.localPosition = new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian));
            weaponRendererTransform.localRotation = Quaternion.Euler(0f, 0f, radian * Mathf.Rad2Deg);
        }

        public void AttackWithTargetPosition(Vector3 position)
        {
            if (handledWeapon != null)
                handledWeapon.OnAttack(cachedTransform.position, (position - cachedTransform.position).normalized);
        }

        public void OnDrawGizmos()
        {
            if (handledWeapon != null)
                handledWeapon.AttackType.OnDrawGizmos();
        }
    }
}