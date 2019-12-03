using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _cameraTransform;


    private void LateUpdate ()
    {
        transform.localRotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles);
    }
}