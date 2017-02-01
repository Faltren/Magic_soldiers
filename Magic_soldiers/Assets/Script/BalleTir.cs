using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleTir : MonoBehaviour {

    public int ejectSpeed = 20;
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i;

    public Rigidbody balleCasting;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            Fire();
    }

    private void Fire()
    {

        nextFire = Time.time + fireRate;

        Rigidbody balle;

        i++;

        balle = Instantiate(balleCasting, transform.position, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.right * ejectSpeed);
        balle.isKinematic = false;

        balle.name = "Bullet " + i;


    }

    
        
        



}
