using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gnome.Isometric.Prototype
{
    public class BulletPrototype : MonoBehaviour
    {
        private Vector3 velocity;

        private Rigidbody bulletRigidbody;

        private void Awake()
        {
            bulletRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            bulletRigidbody.velocity = velocity;
        }

        public void Initialize(Vector3 position, Vector3 velocity)
        {
            bulletRigidbody.position = position;

            this.velocity = velocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.name.Equals("Player") && !other.gameObject.name.Equals("Bullet(Clone)"))
            {
                GameObject.Destroy(gameObject);

                ITarget target = other.GetComponent<ITarget>();

                if (target != null)
                    target.InflictDamage(10f);
            }
        }
    }
}