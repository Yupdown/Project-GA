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

            transform.rotation = Quaternion.Euler(eulerB);
            transform.localScale = new Vector3(Mathf.Repeat(eulerA.y - eulerB.y + 180f, 360f) < 180f ? 1f : -1f, 1f, 1f);
        }
    }
}