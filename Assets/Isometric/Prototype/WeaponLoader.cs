using UnityEngine;
using Gnome.Template;

namespace Gnome.Isometric.Prototype
{
    public class WeaponLoader : MonoBehaviour
    {
        [SerializeField]
        private TextAsset weaponTemplateCSVAsset;

        private void Awake()
        {
            string csvText = weaponTemplateCSVAsset.text;

            Weapon[] weapons = CSVTemplateInstantiation.CreateInstancesFromText<Weapon>(csvText);

            foreach (var weapon in weapons)
                Debug.Log(weapon.WeaponName);
        }
    }
}