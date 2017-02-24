using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleTir : MonoBehaviour {

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i; //name Compteur
    private int nbTirs;

    public static bool isSurchauffe;


    public Rigidbody balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;

    // Use this for initialization
    void Start () {
        isSurchauffe = false;
        shoot = GetComponentInChildren<ParticleSystem>();
        nbTirs = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextFire)
        {
            isSurchauffe = false;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
                nbTirs = 0;
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                if (nbTirs < 20)
                {
                    Burst_Fire();
                }
                else
                {
                    isSurchauffe = true;
                    surchauffe.Play();
                    nbTirs = 0;
                    nextFire = Time.time + 2.5f;
                    fireRate = 0.36f;
                }
            }

        }

         
            
    }

    private void Fire()
    {
        fireRate = 0.36f;

        nextFire = Time.time + fireRate;

        Rigidbody balle;

        i++;

        Vector3 trans = transform.position;
        trans.x += 1; //deplacement de la balle a cause du mesh collider

        balle = Instantiate(balleCasting, trans, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.right * ejectSpeed);
        balle.isKinematic = false;

        balle.name = "Bullet " + i;

        
        shoot.Play();
    }


    private void Burst_Fire()
    {
        fireRate = 0.1f;

        nbTirs++;

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
