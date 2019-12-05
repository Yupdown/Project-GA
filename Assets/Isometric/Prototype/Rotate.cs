using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Rotate(0f, 180f * Time.deltaTime, 0f, Space.World);
	}
}
