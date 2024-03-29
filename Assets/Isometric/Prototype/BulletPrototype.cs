﻿using System.Collections;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.name.Equals("Player"))
            {
                GameObject.Destroy(gameObject);

                ITarget target = collision.gameObject.GetComponent<ITarget>();

                if (target != null)
                    target.InflictDamage(10f);
            }
        }
    }
}