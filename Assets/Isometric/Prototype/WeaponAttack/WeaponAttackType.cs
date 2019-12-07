using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public abstract class WeaponAttackType
    {
        public abstract void OnAttack(Vector3 originPosition, Vector3 direction);

        public virtual void OnDrawGizmos() { }

        public virtual CursorType WeaponCursor
        {
            get
            { return CursorType.Default; }
        }
    }
}
