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

        public override void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            Vector3 euler = Random.insideUnitSphere * accuracy;
            Vector3 spreadDirection = Quaternion.Euler(euler) * direction;

            Ray ray = new Ray(originPosition, spreadDirection.normalized);

            RaycastHit[] result = Physics.RaycastAll(ray);
            
            GameObject rayInstance = GameObject.Instantiate(rayPrefab);
            LineRenderer rayComponent = rayInstance.GetComponent<LineRenderer>();

            rayComponent.positionCount = 2;
            rayComponent.useWorldSpace = true;
            rayComponent.SetPosition(0, originPosition);
            rayComponent.SetPosition(1, originPosition + spreadDirection * 10f);

            foreach (RaycastHit hit in result)
            {
                ITarget target = hit.collider.GetComponent<ITarget>();

                if (target != null)
                {
                    InflictDamage(target);

                    if (!piercing)
                        break;
                }
            }

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