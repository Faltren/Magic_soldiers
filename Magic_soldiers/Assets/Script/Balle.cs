using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour {

    private Rigidbody balle;
    private GameObject balleGb;
    private Object balleObj;

    // Use this for initialization
    void Start () {
        balle = GetComponent<Rigidbody>();
        balleGb = GetComponent<GameObject>();
        balleObj = GetComponent<Object>();
        balle.tag = "balle";
	}
	
	// Update is called once per frame
	void Update () {

        if (balle.transform.position.x > 1000 || balle.transform.position.y > 1000 || balle.transform.position.z > 1000)
        {
            Destroy((balleObj as Transform).gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy((balleObj as Transform).gameObject);
    }


}
