using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackMeleeRadial : WeaponAttackType
    {
        private const float CastIntervalMax = 0.1f;
        private const float CastTerrainHeight = 0.1f;

        private float radius;
        private float angle;

        public WeaponAttackMeleeRadial(float radius, float angle)
        {
            this.radius = radius;
            this.angle = angle;
        }

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            //int rayCount = Mathf.Max(Mathf.FloorToInt(radius * 2f * Mathf.PI * (angle / 360f) / CastIntervalMax) + 1, 2);
            //float[] distances = new float[rayCount];

            //for (int count = 0; count < rayCount; count++)
            //{
            //    Vector3 rayDirection = Quaternion.AngleAxis(((float)count / (rayCount - 1) - 0.5f) * angle, Vector3.up) * direction;

            //    Ray ray = new Ray(originPosition, rayDirection);

            //    RaycastHit result;
            //    Physics.Raycast(ray, out result, radius, 24);

            //    distances[count] = result.distance;
            //}

            Collider[] result = Physics.OverlapSphere(originPosition, radius);

            foreach (Collider collider in result)
            {
                ITarget target = collider.GetComponent<ITarget>();

                if (target != null)
                {
                    Transform targetTransform = collider.GetComponent<Transform>();
                    Vector3 targetPosition = targetTransform.position;

                    Vector2 lhs = new Vector2(targetPosition.x - originPosition.x, targetPosition.z - originPosition.z).normalized;
                    Vector2 rhs = new Vector2(direction.x, direction.z).normalized;

                    if (Vector2.Dot(lhs, rhs) > 1f - angle / 180f)
                        target.InflictDamage(10f);
                }
            }

            gizmosPosition = originPosition;
            gizmosDirection = direction;
        }

        private Vector3 gizmosPosition;
        private Vector3 gizmosDirection;

        public override void OnDrawGizmos()
        {
            Quaternion orientation = Quaternion.Euler(0f, -Mathf.Atan2(gizmosDirection.z, gizmosDirection.x) * Mathf.Rad2Deg, 0f);

            Vector3[] points = new Vector3[3];

            points[0] = Vector3.zero;
            points[1] = Quaternion.Euler(0f, angle * 0.5f, 0f) * Vector3.right * radius;
            points[2] = Quaternion.Euler(0f, -angle * 0.5f, 0f) * Vector3.right * radius;

            for (int index = 0; index < points.Length; index++)
                points[index] = orientation * points[index] + gizmosPosition;

            Gizmos.DrawWireSphere(gizmosPosition, radius);
            Gizmos.DrawLine(points[0], points[1]);
            Gizmos.DrawLine(points[0], points[2]);
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.MeleeWeapon; }
        }
    }
}