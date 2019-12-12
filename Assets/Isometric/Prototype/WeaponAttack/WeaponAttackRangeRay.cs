using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class WeaponAttackRangeRay : WeaponAttackType
    {
        private float accuracy;
        private bool piercing;

        private GameObject rayPrefab;

        public WeaponAttackRangeRay(float attackDamage, float attackSpeed, float accuracy, bool piercing) : base(attackDamage, attackSpeed)
        {
            this.accuracy = accuracy;
            this.piercing = piercing;

            rayPrefab = Resources.Load<GameObject>("Ray Line Renderer");
        }

        public override void OnAttack(Vector3 originPosition, Vector3 targetPosition)
        {
            Vector3 euler = Random.insideUnitSphere * accuracy * 0.5f;
            Vector3 spreadDirection = Quaternion.Euler(euler) * (targetPosition - originPosition).normalized;

            Ray ray = new Ray(originPosition, spreadDirection);
            Vector3 hitPosition = originPosition + spreadDirection * 10f;

            if (piercing)
            {
                RaycastHit[] result = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in result)
                {
                    ITarget target = hit.collider.GetComponent<ITarget>();

                    if (target != null)
                        InflictDamage(target);
                }
            }
            else
            {
                RaycastHit result;

                if (Physics.Raycast(ray, out result))
                {
                    hitPosition = result.point;

                    ITarget target = result.collider.GetComponent<ITarget>();

                    if (target != null)
                        InflictDamage(target);
                }
            }

            GameObject rayInstance = GameObject.Instantiate(rayPrefab);
            LineRenderer rayComponent = rayInstance.GetComponent<LineRenderer>();

            rayComponent.positionCount = 2;
            rayComponent.useWorldSpace = true;
            rayComponent.SetPosition(0, originPosition);
            rayComponent.SetPosition(1, hitPosition);

            gizmosRay = ray;
        }

        private Ray gizmosRay;

        public override void OnDrawGizmos()
        {
            Gizmos.DrawRay(gizmosRay);
        }

        public override CursorType WeaponCursor
        {
            get
            { return CursorType.RangeWeapon; }
        }
    }
}