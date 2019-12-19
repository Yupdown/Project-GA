using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class PlayerRotation : MonoBehaviour
    {
        private PlayerMovement playerMovement;

        private PlayerWeaponHandler playerWeaponHandler;

        [SerializeField]
        private Transform mecanimTransform;

        [SerializeField]
       private Transform weaponTransform;

        [SerializeField]
        private Transform weaponRendererTransform;

        private float viewAngle;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        }

        private void Update()
        {
            viewAngle = Mathf.LerpAngle(viewAngle, playerWeaponHandler.AimPriority ? playerWeaponHandler.ViewAngle : playerMovement.ViewAngle, Time.deltaTime * 8f);

            float cameraAngle = IsometricCameraBehaviour.cameraTransform.eulerAngles.y - 45f;
            float weaponAngle = viewAngle + cameraAngle + playerWeaponHandler.SwingValue;

            if (weaponTransform.localScale.x < 0f)
                weaponAngle = weaponAngle * -1f + 90f;
            weaponRendererTransform.localRotation = Quaternion.Euler(0f, 0f, weaponAngle);
            mecanimTransform.localRotation = Quaternion.Euler(0f, -(viewAngle + 90f), 0f);
        }
    }
}