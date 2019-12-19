using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackTarget : WeaponAttackType
    {
        private float attackRadius;
        private float attackRange;

        public WeaponAttackTarget(float attackDamage, float attackSpeed, float attackRadius, float attackRange) : base(attackDamage, attackSpeed)
        {
            this.attackRadius = attackRadius;
            this.attackRange = attackRange;
        }

        public override void OnAttack(Vector3 originPosition, Vector3 targetPosition)
        {
            Collider[] result = Physics.OverlapSphere(targetPosition, attackRange);

            foreach (Collider collider in result)
            {
                ITarget target = collider.GetComponent<ITarget>();

                if (target != null)
                    InflictDamage(target);
            }

            gizmosPosition = targetPosition;
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
