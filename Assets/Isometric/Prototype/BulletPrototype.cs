using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrototype : MonoBehaviour
{
    private Vector3 velocity;

    private void Update()
    {
        transform.localPosition += velocity * Time.deltaTime;
    }

    public void Initialize (Vector3 position, Vector3 velocity)
    {
        transform.localPosition = position;

        this.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}