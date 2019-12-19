using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform cameraTransform;

        private void Start()
        {
            cameraTransform = IsometricCameraBehaviour.cameraTransform;
        }

        private void LateUpdate()
        {
            Vector3 eulerA = transform.parent.eulerAngles;
            Vector3 eulerB = cameraTransform.eulerAngles;

            UpdateViewAngle(eulerA, eulerB);
        }

        public virtual void UpdateViewAngle(Vector3 thisEuler, Vector3 cameraEuler)
        {
            bool invertX = Mathf.Repeat(thisEuler.y - cameraEuler.y + 180f, 360f) > 180f;

            transform.rotation = Quaternion.Euler(cameraEuler);
            transform.localScale = new Vector3(invertX ? -1f : 1f, 1f, 1f);
        }
    }
}