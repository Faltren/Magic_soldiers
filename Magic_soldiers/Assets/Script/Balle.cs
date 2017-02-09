using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour {

    private Rigidbody balle;
    private Object balleObj;

    public static bool IsDestroyed = false;

    // Use this for initialization
    void Start () {
        balleObj = GetComponent<Object>();
        balle = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (balle.position.x > 1000 || balle.transform.position.y > 1000 || balle.transform.position.z > 1000 || balle.position.x < -1000 || balle.transform.position.y < -1000 || balle.transform.position.z < -1000)
        {
            Destroy((balleObj as Transform).gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        IsDestroyed = true;
        Destroy((balleObj as Transform).gameObject);
    }


}
