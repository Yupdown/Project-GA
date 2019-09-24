using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 deltaVector = new Vector3(horizontal + vertical, 0f, vertical - horizontal) * 10f * Time.deltaTime;

        _transform.localPosition += deltaVector;
	}
}