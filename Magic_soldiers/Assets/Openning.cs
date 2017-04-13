using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openning : MonoBehaviour
{    
    public GameObject door;
    public GameObject RotateAxe;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        door.transform.RotateAround(door.transform.position, RotateAxe.transform.position, 90f*Time.time*10);
	}
}
