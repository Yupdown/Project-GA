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

        [SerializeField]
        private IsometricCameraBehaviour cameraBehaviour;

        private Transform cachedTransform;

        private float scrollAmount;

        private float lastAttackTime;
        private bool attackCycle;
        private float swingValue;
        private float rotationValue;

        private void Awake()
        {
            registeredWeapons = weaponLoader.LoadWeapons();

            cachedTransform = GetComponent<Transform>();

            lastAttackTime = 0f;
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

            float attackTime = Time.time - lastAttackTime;

            if (attackTime < 1.5f)
                swingValue = Mathf.Lerp(swingValue, attackCycle ? 60f : -60f, Time.deltaTime * 10f);
            else
                swingValue = Mathf.Lerp(swingValue, 0f, Time.deltaTime * 5f);

            weaponRendererTransform.localRotation = Quaternion.Euler(0f, 0f, rotationValue + swingValue);
        }

        public void SwitchWeapon(Weapon weapon)
        {
            handledWeapon = weapon;
            weaponRenderer.sprite = weapon.WeaponTexture;

            playerInterface.SwitchCursor(weapon.AttackType.WeaponCursor);
            playerInterface.UpdateWeapon(weapon);
        }

        public void AimWeapon(Vector3 position)
        {
            Vector3 delta = position - cachedTransform.position;
            float radian = Mathf.Atan2(delta.z, delta.x);

            rotationValue = radian * Mathf.Rad2Deg + IsometricCameraBehaviour.cameraTransform.eulerAngles.y - 45f;
            if (weaponTransform.localScale.x < 0f)
                rotationValue = rotationValue * -1f + 90f;

            weaponTransform.localPosition = new Vector3(Mathf.Cos(radian), 0f, Mathf.Sin(radian)) * 0.2f;

            cameraBehaviour.OffsetCamera((Input.mousePosition - new Vector3(Screen.width, Screen.height) * 0.5f) / Screen.width);
        }

        public void AttackWithTargetPosition(Vector3 position)
        {
            if (handledWeapon != null)
            {
                if (Time.time - lastAttackTime >= handledWeapon.AttackSpeed)
                {
                    handledWeapon.OnAttack(cachedTransform.position, position);
                    cameraBehaviour.ShakeCamera(0.2f);
                    attackCycle = !attackCycle;

                    lastAttackTime = Time.time;
                }
            }
        }

        public void OnDrawGizmos()
        {
            if (handledWeapon != null)
                handledWeapon.AttackType.OnDrawGizmos();
        }
    }
}