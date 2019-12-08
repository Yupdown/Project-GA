using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackTarget : WeaponAttackType
    {
        private float attackRange;

        public WeaponAttackTarget(float attackDamage, float attackSpeed, float attackRange) : base(attackDamage, attackSpeed)
        {
            this.attackRange = attackRange;
        }

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            Collider[] result = Physics.OverlapSphere(originPosition, attackRange);

            foreach (Collider collider in result)
            {
                ITarget target = collider.GetComponent<ITarget>();

                if (target != null)
                {
                    InflictDamage(target);
                    break;
                }
            }

            gizmosPosition = originPosition;
        }

        private Vector3 gizmosPosition;

        public override void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(gizmosPosition, attackRange);
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.MeleeWeapon; }
        }
    }
}
