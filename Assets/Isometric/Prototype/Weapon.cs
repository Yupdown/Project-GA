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
                attackType.Attack(originPosition, direction);
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
                case 0: attackType = new WeaponAttackTarget(attackDamage, attackSpeed, attackRangeValue1);
                    break;
                case 1: attackType = new WeaponAttackMeleeRadial(attackDamage, attackSpeed, attackRangeValue1, attackRangeValue2);
                    break;
                case 2: attackType = new WeaponAttackMeleeBox(attackDamage, attackSpeed, attackRangeValue1, attackRangeValue2);
                    break;
                case 3: attackType = new WeaponAttackRangeRay(attackDamage, attackSpeed, attackRangeValue1, attackRangeValue2 > 0f);
                    break;
                case 4: attackType = new WeaponAttackRangeProjectile(attackDamage, attackSpeed, attackRangeValue1, attackRangeValue2);
                    break;
            }
        }
    }
}