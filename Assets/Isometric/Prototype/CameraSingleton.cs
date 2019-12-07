using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    public static Transform cameraTransform;
    
    private void Awake()
    {
        cameraTransform = GetComponent<Transform>();
    }
}
