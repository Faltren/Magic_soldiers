using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour {

    private Rigidbody balle;
    private Object balleObj;

    private int max = 1500;

    public static bool IsDestroyed = false;

    void Start () {
        balleObj = GetComponent<Object>();
        balle = GetComponent<Rigidbody>();
    }
	
	void Update () {

        if ((balle.position.x > max || balle.transform.position.y > max || balle.transform.position.z > max || balle.position.x < -max || balle.transform.position.y < -max || balle.transform.position.z < -max) && balle.velocity != new Vector3(0,0,0))
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
