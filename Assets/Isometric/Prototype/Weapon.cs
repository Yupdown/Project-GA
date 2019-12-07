using UnityEngine;
using Gnome.Template;

namespace Gnome.Isometric.Prototype
{
    internal class Weapon : ITemplateOverride
    {
        public string WeaponName
        {
            get
            { return weaponName; }
        }
        private string weaponName;

        public Sprite WeaponTexture
        {
            get
            { return weaponTexture; }
        }
        private Sprite weaponTexture;

        private int attackDamage;
        private float attackSpeed;

        public WeaponAttackType AttackType
        {
            get
            { return attackType; }
        }
        private WeaponAttackType attackType;

        public Weapon()
        {
            weaponName = string.Empty;
            attackDamage = 10;
            attackSpeed = 1.0f;
        }

        public void OnAttack(Vector3 originPosition, Vector3 direction)
        {
            if (attackType != null)
                attackType.OnAttack(originPosition, direction);
        }

        public void Override(TemplateRecord record)
        {
            record.TryGetValue("WeaponName", ref weaponName);
            record.TryGetValue("aAtkDmg", ref attackDamage);
            record.TryGetValue("AtkSpd", ref attackSpeed);

            string texturePath = string.Empty;
            record.TryGetValue("ItemMesh", ref texturePath);
            weaponTexture = Resources.Load<Sprite>(texturePath);

            int attackDirection = 0;
            record.TryGetValue("AttackDirection", ref attackDirection);
            float attackRangeValue1 = 0.0f;
            record.TryGetValue("AttackRangeValue1", ref attackRangeValue1);
            float attackRangeValue2 = 0.0f;
            record.TryGetValue("AttackRangeValue2", ref attackRangeValue2);

            switch (attackDirection)
            {
                case 1:
                    attackType = new WeaponAttackTarget();
                    break;
                case 2:
                    attackType = new WeaponAttackMeleeRadial(attackRangeValue1, attackRangeValue2);
                    break;
                case 3:
                    attackType = new WeaponAttackMeleeBox(attackRangeValue1, attackRangeValue2);
                    break;
                case 4:
                    attackType = new WeaponAttackRangeRay(attackRangeValue1, attackRangeValue2 > 0f);
                    break;
                case 5:
                    attackType = new WeaponAttackRangeProjectile(attackRangeValue1, attackRangeValue2);
                    break;
            }
        }
    }
}