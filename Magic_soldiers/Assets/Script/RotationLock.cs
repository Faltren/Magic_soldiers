using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour {

    private int sensi = Personnage.sensibility;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Input.GetAxisRaw("Mouse Y") * sensi, Input.GetAxisRaw("Mouse Y") * sensi, Input.GetAxisRaw("Mouse Y") * -sensi);
	}
}
