using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public abstract class WeaponAttackType
    {
        protected float attackDamage = 10f;

        protected float attackSpeed = 1f;

        private float lastAttackTime;

        public WeaponAttackType(float attackDamage, float attackSpeed)
        {
            this.attackDamage = attackDamage;
            this.attackSpeed = attackSpeed;

            lastAttackTime = 0f;
        }

        public void InflictDamage(ITarget targetObject)
        {
            targetObject.InflictDamage(attackDamage);
        }

        public void Attack(Vector3 originPosition, Vector3 direction)
        {
            if (Time.time - lastAttackTime >= attackSpeed)
            {
                OnAttack(originPosition, direction);

                lastAttackTime = Time.time;
            }
        }

        public abstract void OnAttack(Vector3 originPosition, Vector3 direction);

        public virtual void OnDrawGizmos() { }

        public virtual CursorType WeaponCursor
        {
            get
            { return CursorType.Default; }
        }
    }
}
