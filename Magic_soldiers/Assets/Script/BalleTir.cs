using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleTir : MonoBehaviour {

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i;

    public Rigidbody balleCasting;
    private ParticleSystem shoot;

    // Use this for initialization
    void Start () {
        shoot = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            Fire();
        if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire)
            Burst_Fire();
    }

    private void Fire()
    {
        fireRate = 0.36f;

        nextFire = Time.time + fireRate;

        Rigidbody balle;

        i++;
        
        balle = Instantiate(balleCasting, transform.position, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.right * ejectSpeed);
        balle.isKinematic = false;

        balle.name = "Bullet " + i;

        
        shoot.Play();
    }


    private void Burst_Fire()
    {
        fireRate = 0.1f;

        nextFire = Time.time + fireRate;

        Rigidbody balle;

        i++;
        
        balle = Instantiate(balleCasting, transform.position, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.right * ejectSpeed);
        balle.isKinematic = false;

        balle.name = "Bullet " + i;

        shoot.Play();
    }







}
