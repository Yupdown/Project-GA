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

        private int attackDamage;
        private float attackSpeed;

        private WeaponAttackType attackType;

        public Weapon()
        {
            weaponName = string.Empty;
            attackDamage = 10;
            attackSpeed = 1.0f;
        }

        public void Override(TemplateRecord record)
        {
            record.TryGetValue("WeaponName", ref weaponName);
            record.TryGetValue("aAtkDmg", ref attackDamage);
            record.TryGetValue("AtkSpd", ref attackSpeed);
            // record.TryGetValue("AttackDirection", ref attackDirection);
        }
    }
}