using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackMeleeBox : WeaponAttackType
    {
        private float distance;
        private float width;

        public WeaponAttackMeleeBox(float distance, float width)
        {
            this.distance = distance;
            this.width = width;
        }

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            Quaternion orientation = Quaternion.Euler(0f, -Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg, 0f);
            Vector3 halfExtents = new Vector3(distance * 0.5f, 0.5f, width * 0.5f);

            Collider[] result = Physics.OverlapBox(originPosition + (orientation * new Vector3(halfExtents.x, halfExtents.y, 0f)), halfExtents, orientation);

            foreach (Collider collider in result)
            {
                ITarget target = collider.GetComponent<ITarget>();

                if (target != null)
                    target.InflictDamage(10f);
            }

            gizmosPosition = originPosition;
            gizmosDirection = direction;
        }


        private Vector3 gizmosPosition;
        private Vector3 gizmosDirection;

        public override void OnDrawGizmos()
        {
            Quaternion orientation = Quaternion.Euler(0f, -Mathf.Atan2(gizmosDirection.z, gizmosDirection.x) * Mathf.Rad2Deg, 0f);
            Vector3 halfExtents = new Vector3(distance * 0.5f, 0.5f, width * 0.5f);

            Vector3[] points = new Vector3[8];

            points[0] = new Vector3(0f, 0f, halfExtents.z);
            points[1] = new Vector3(0f, 0f, -halfExtents.z);
            points[2] = new Vector3(halfExtents.x * 2f, 0f, halfExtents.z);
            points[3] = new Vector3(halfExtents.x * 2f, 0f, -halfExtents.z);
            points[4] = new Vector3(0f, halfExtents.y * 2f, halfExtents.z);
            points[5] = new Vector3(0f, halfExtents.y * 2f, -halfExtents.z);
            points[6] = new Vector3(halfExtents.x * 2f, halfExtents.y * 2f, halfExtents.z);
            points[7] = new Vector3(halfExtents.x * 2f, halfExtents.y * 2f, -halfExtents.z);

            for (int index = 0; index < points.Length; index++)
                points[index] = orientation * points[index] + gizmosPosition;

            Gizmos.DrawLine(points[0], points[1]);
            Gizmos.DrawLine(points[1], points[3]);
            Gizmos.DrawLine(points[3], points[2]);
            Gizmos.DrawLine(points[2], points[0]);
            Gizmos.DrawLine(points[0], points[4]);
            Gizmos.DrawLine(points[1], points[5]);
            Gizmos.DrawLine(points[2], points[6]);
            Gizmos.DrawLine(points[3], points[7]);
            Gizmos.DrawLine(points[4], points[5]);
            Gizmos.DrawLine(points[5], points[7]);
            Gizmos.DrawLine(points[7], points[6]);
            Gizmos.DrawLine(points[6], points[4]);
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.MeleeWeapon; }
        }
    }
}
