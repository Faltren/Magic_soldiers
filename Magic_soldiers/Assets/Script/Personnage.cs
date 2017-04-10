﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Personnage : NetworkBehaviour {

    #region Attributes

    public int personnageSpeedWalk;
    public int personnageSpeedRun;
    private int personnageSpeed;
    public int jumpSpeed;
    private bool isGrounded;
    
    public AudioClip jump;
    public AudioClip land;
    public AudioSource sound;
    
    //mouvement de tete : sensi
    public static int sensibility = 5;

    //life/attack/shield
    public static int life;
    public static int attack;
    public static int shield;

    private Rigidbody player;
    public GameObject Spine;
    public static Animation anim;

    private Camera cam;
    private AudioListener al;

    //Tir

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i; //name Compteur
    private int nbTirs;

    private Vector3 trans;

    public static bool isSurchauffe;
    BalleTir blabla;
    public Rigidbody balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;
    public GameObject BulletPos;
    #endregion



    #region Unity methods

    void Start () {

        blabla = new BalleTir();

        life = 100;
        shield = 100;
        attack = 5;

        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        sound = GetComponent<AudioSource>();

        cam = GetComponentInChildren<Camera>();
        al = GetComponentInChildren<AudioListener>();

        cam.transform.SetParent(Spine.transform);

        isGrounded = false;
        attack = 5;

        isSurchauffe = false;
        shoot = GetComponentInChildren<ParticleSystem>();
        nbTirs = 0;
    }
	
	void FixedUpdate () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (isLocalPlayer)
        {
            if (!cam.enabled)
                cam.enabled = true;
            if (!al.enabled)
                al.enabled = true;
        }
        


        if (isLocalPlayer)
        {
            Moves();
            AnimPerso();
            /*if (Time.time > nextFire)
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

            }*/
        }
        

	}
    #endregion


    #region otherMethods


    private void Moves()
    {
        //Deplacement
        transform.Translate(Input.GetAxis("Horizontal") * personnageSpeed * Time.deltaTime , 0, Input.GetAxis("Vertical") * personnageSpeed * Time.deltaTime);
        
        //Jump
        if (Input.GetAxis("Jump") != 0f && isGrounded)
        {
            player.AddForce(0, jumpSpeed, 0);
            sound.PlayOneShot(jump);
            isGrounded = false;
        }
        
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            personnageSpeed = personnageSpeedRun;   
        }
        else
        {
            personnageSpeed = personnageSpeedWalk;
        }        

        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);

        
        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            Spine.transform.Rotate(0, 0, Input.GetAxisRaw("Mouse Y") * sensibility);
        }

        if (Spine.transform.eulerAngles.z > 330)
        {
            Spine.transform.eulerAngles = new Vector3(
                  Spine.transform.eulerAngles.x,
                   Spine.transform.eulerAngles.y,
                   330);
        }

        if (Spine.transform.eulerAngles.z < 200)
        {
            Spine.transform.eulerAngles = new Vector3(
                   Spine.transform.eulerAngles.x,
                   Spine.transform.eulerAngles.y,
                   200);
        }

        //debug des forces
        player.velocity = new Vector3(0, player.velocity.y, 0);

    }

    private void AnimPerso()
    {

        if (BalleTir.isSurchauffe)
        {
            anim["surchauffe"].speed = 0.7f;
            anim.Play("surchauffe");
        }
        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            anim.Play("assault_combat_run");
        else
            anim.Stop();*/

        
    }



    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        player.velocity = new Vector3(0, 0, 0);
    }

    private void Fire()
    {
        fireRate = 0.36f;

        nextFire = Time.time + fireRate;

        Rigidbody balle;

        i++;

        //Quaternion qua = new Quaternion(0, 0, 0, 0);

        balle = Instantiate(balleCasting, BulletPos.transform.position, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.forward) * ejectSpeed;
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

        balle = Instantiate(balleCasting, BulletPos.transform.position, Quaternion.identity);
        balle.velocity = transform.TransformDirection(Vector3.forward * ejectSpeed);
        balle.isKinematic = false;

        balle.name = "Bullet " + i;

        

        shoot.Play();
    }
    #endregion

}



//private Vector3 directionMove = Vector3.zero;

//directionMove.z = directionMove.z + Input.GetAxis("Vertical") * personnageSpeed * Time.deltaTime; //avant / arriere
//directionMove.x = directionMove.x + Input.GetAxis("Horizontal") * personnageSpeed * Time.deltaTime; //gauche / droite


//deplacement
//directionMove = transform.TransformDirection(directionMove.x * personnageSpeed, directionMove.y, directionMove.z * personnageSpeed);
//directionMove *= Time.deltaTime;

/*if (!player.isGrounded)
    directionMove.y -= gravity * Time.deltaTime;*/

//player.Move(directionMove);