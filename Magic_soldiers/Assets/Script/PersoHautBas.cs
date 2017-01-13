using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersoHautBas : MonoBehaviour {

    private int sensi = Personnage.sensibility;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0,0,Input.GetAxisRaw("Mouse Y") * sensi);

	}
}
