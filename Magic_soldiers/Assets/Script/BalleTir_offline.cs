﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BalleTir_offline : MonoBehaviour
{

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i; //name Compteur
    private int nbTirs;

    private Vector3 trans;

    public static bool isSurchauffe;


    public Rigidbody balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;

    void Start () {
        isSurchauffe = false;
        shoot = GetComponentInChildren<ParticleSystem>();
        nbTirs = 0;
    }
	
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

        Quaternion qua = new Quaternion(0, 0, 0, GetComponentInParent<Rigidbody>().transform.rotation.x);

        //print("Quaternion : " + qua);
        //print("Rigidbody : " + GetComponentInParent<Rigidbody>().name);

        balle = Instantiate(balleCasting, transform.position, qua);
        balle.velocity = transform.TransformDirection(Vector3.right) * ejectSpeed;
        balle.isKinematic = false;

        //print("position : " + balle.transform.position);
        //print("rotation : " + balle.transform.rotation);
        //print("rotation qua : " + qua);

        balle.transform.rotation = new Quaternion(0, 0, 0, 0);

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
