using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class IsometricCameraBehaviour : MonoBehaviour
    {
        public static Transform cameraTransform;
        public static Camera cameraInstance;

        private float shakeValue;
        private Vector2 offset;

        private void Awake()
        {
            cameraTransform = GetComponent<Transform>();
            cameraInstance = GetComponent<Camera>();
        }

        private void Update()
        {
            shakeValue = Mathf.Lerp(shakeValue, 0f, Time.deltaTime * 10f);

            Vector2 shakeOffset = shakeValue > 0.01f ? Random.insideUnitCircle * shakeValue : Vector2.zero;

            cameraTransform.localPosition = offset + shakeOffset;
        }

        public void ShakeCamera(float value)
        {
            shakeValue = value;
        }

        public void OffsetCamera(Vector2 offset)
        {
            this.offset = offset;
        }
    }
}