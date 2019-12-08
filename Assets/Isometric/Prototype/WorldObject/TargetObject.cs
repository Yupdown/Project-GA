using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class TargetObject : WorldObject, ITarget
    {
        [SerializeField]
        private Transform rendererTransform;

        private float damageTime;

        private void Update()
        {
            damageTime += Time.deltaTime;

            rendererTransform.localPosition = Vector3.up * Mathf.Sin(Mathf.Min(damageTime * 10f, Mathf.PI));
        }

        public void InflictDamage(float value)
        {
            damageTime = 0f;
        }
    }

    public interface ITarget
    {
        void InflictDamage(float value);
    }
}