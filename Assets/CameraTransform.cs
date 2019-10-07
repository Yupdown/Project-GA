using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private Vector3 _targetPosition;

    private float _viewAngle = 45f;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeAngle(-90f);
        else if (Input.GetKeyDown(KeyCode.E))
            ChangeAngle(90f);
    }

    private void LateUpdate()
    {
        float angleY = Mathf.LerpAngle(_transform.localRotation.eulerAngles.y, _viewAngle, Time.deltaTime * 8f);

        _transform.localRotation = Quaternion.Euler(0f, angleY, 0f);
    }

    public void ChangeAngle(float delta)
    {
        _viewAngle += delta;
    }
}