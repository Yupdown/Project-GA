using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackRangeProjectile : WeaponAttackType
    {
        private float projectileSpeed;
        private float projectileRadius;

        private GameObject bulletPrefab;

        public WeaponAttackRangeProjectile(float projectileSpeed, float projectileRadius)
        {
            this.projectileSpeed = projectileSpeed;
            this.projectileRadius = projectileRadius;

            bulletPrefab = Resources.Load<GameObject>("Bullet");
        }

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletPrefab);
            BulletPrototype bulletComponent = bulletInstance.GetComponent<BulletPrototype>();
            
            bulletComponent.Initialize(originPosition, direction.normalized * projectileSpeed);
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.RangeWeapon; }
        }
    }
}
