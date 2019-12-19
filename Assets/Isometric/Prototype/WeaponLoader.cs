using UnityEngine;
using Gnome.Template;

namespace Gnome.Isometric.Prototype
{
    internal class WeaponLoader : MonoBehaviour
    {
        [SerializeField]
        private TextAsset weaponTemplateCSVAsset;

        public Weapon[] LoadWeapons()
        {
            string csvText = weaponTemplateCSVAsset.text;

            Weapon[] weapons = CSVTemplateInstantiation.CreateInstancesFromText<Weapon>(csvText);

            return weapons;
        }
    }
}