using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour {

    private Rigidbody balle;

	// Use this for initialization
	void Start () {
        balle = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (balle.transform.position.x > 1000 || balle.transform.position.y > 1000 || balle.transform.position.z > 1000)
        {
            Destroy(balle);
        }

    }
}
