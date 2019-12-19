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
        private SpriteRenderer weaponRenderer;

        [SerializeField]
        private IsometricCameraBehaviour cameraBehaviour;

        private Transform cachedTransform;

        private float scrollAmount;

        private float lastAttackTime;
        private bool attackCycle;
        private float rotationValue;

        private float offsetValue;

        public float ViewAngle
        {
            get
            { return viewAngle; }
        }
        private float viewAngle;

        public bool AimPriority
        {
            get
            { return aimPriority; }
        }
        private bool aimPriority;

        public float SwingValue
        {
            get
            { return swingValue; }
        }
        private float swingValue;

        private void Awake()
        {
            registeredWeapons = weaponLoader.LoadWeapons();

            cachedTransform = GetComponent<Transform>();

            lastAttackTime = 0f;
            offsetValue = 1f;
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

            bool canSwing = false;
            bool canAiming = false;

            if (handledWeapon != null)
            {
                canSwing = handledWeapon.AttackType.WeaponCursor == CursorType.MeleeWeapon;
                canAiming = handledWeapon.AttackType.WeaponCursor == CursorType.RangeWeapon;
            }

            bool attacking = attackTime < 0.5f;

            if (canSwing && attacking)
                swingValue = Mathf.Lerp(swingValue, attackCycle ? 60f : -60f, Time.deltaTime * 10f);
            else
                swingValue = Mathf.Lerp(swingValue, 0f, Time.deltaTime * 5f);
            
            bool aiming = Input.GetKey(KeyCode.Mouse1) && canAiming;
            aimPriority = aiming || attacking;

            offsetValue = Mathf.Lerp(offsetValue, aiming ? 8f : 1f, Time.deltaTime * 8f);

            Vector2 offset = (Input.mousePosition - new Vector3(Screen.width, Screen.height) * 0.5f) / Screen.width * offsetValue;

            cameraBehaviour.OffsetCamera(offset);
        }

        public void SwitchWeapon(Weapon weapon)
        {
            handledWeapon = weapon;
            weaponRenderer.sprite = weapon.WeaponTexture;

            playerInterface.SwitchCursor(weapon.AttackType.WeaponCursor);
            playerInterface.UpdateWeapon(weapon);
        }

        public void AimWeapon(Vector3 point)
        {
            Vector3 delta = point - cachedTransform.position;
            AimWeapon(Mathf.Atan2(delta.z, delta.x) * Mathf.Rad2Deg);
        }

        public void AimWeapon(float viewAngle)
        {
            this.viewAngle = viewAngle;
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

        public float ApplyPlayerMoveSpeed(float moveSpeed, PlayerMovement movement)
        {
            if (aimPriority)
                moveSpeed *= 1f - Mathf.Abs(Mathf.DeltaAngle(viewAngle, movement.ViewAngle) / 360f);

            return moveSpeed;
        }
    }
}