using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackRangeRay : WeaponAttackType
    {
        private float accuracy;
        private bool piercing;

        public WeaponAttackRangeRay(float accuracy, bool piercing)
        {
            this.accuracy = accuracy;
            this.piercing = piercing;
        }

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            Ray ray = new Ray(originPosition, direction.normalized);

            RaycastHit[] result = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in result)
            {
                ITarget target = hit.collider.GetComponent<ITarget>();

                if (target != null)
                    target.InflictDamage(10f);
            }
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.RangeWeapon; }
        }
    }
}