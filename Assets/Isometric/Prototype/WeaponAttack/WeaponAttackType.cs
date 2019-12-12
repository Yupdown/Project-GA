using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public abstract class WeaponAttackType
    {
        protected float attackDamage = 10f;

        protected float attackSpeed = 1f;

        public WeaponAttackType(float attackDamage, float attackSpeed)
        {
            this.attackDamage = attackDamage;
            this.attackSpeed = attackSpeed;
        }

        public void InflictDamage(ITarget targetObject)
        {
            targetObject.InflictDamage(attackDamage);
        }

        public void Attack(Vector3 originPosition, Vector3 targetPosition)
        {
            OnAttack(originPosition, targetPosition);
        }

        public abstract void OnAttack(Vector3 originPosition, Vector3 targetPosition);

        public virtual void OnDrawGizmos() { }

        public virtual CursorType WeaponCursor
        {
            get
            { return CursorType.Default; }
        }
    }
}
