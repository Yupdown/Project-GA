using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = CameraSingleton.cameraTransform;
    }

    private void LateUpdate ()
    {
        transform.localRotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles);
    }
}