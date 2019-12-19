using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class CameraFollowZFlip : CameraFollow
    {
        [SerializeField]
        private Sprite frontSprite;
        [SerializeField]
        private Sprite backSprite;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void UpdateViewAngle(Vector3 thisEuler, Vector3 cameraEuler)
        {
            base.UpdateViewAngle(thisEuler, cameraEuler);

            bool invertZ = Mathf.Repeat(thisEuler.y - cameraEuler.y + 90f, 360f) > 180f;
            spriteRenderer.sprite = invertZ ? backSprite : frontSprite;
        }
    }
}