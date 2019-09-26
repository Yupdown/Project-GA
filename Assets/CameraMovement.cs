using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _transform;

    private float _cameraAngle = 45f;

    private Vector2 _cameraPosition;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _cameraPosition += new Vector2(horizontal + vertical, vertical - horizontal) * 10f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            ChangeAngle(90f);
        else if (Input.GetKeyDown(KeyCode.E))
            ChangeAngle(-90f);

        Transform parent = _transform.parent;

        parent.localPosition = new Vector3(_cameraPosition.x, 0f, _cameraPosition.y);
        parent.localRotation = Quaternion.Euler(0f, Mathf.LerpAngle(parent.localRotation.eulerAngles.y, _cameraAngle, Time.deltaTime * 8f), 0f);
	}

    public void ChangeAngle(float delta)
    {
        _cameraAngle += delta;
    }
}