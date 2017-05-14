﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Personnage : NetworkBehaviour {

    #region Attributes

    private GameObject door1;
    private GameObject door2;
    private GameObject door3;
    private GameObject door4;
    private GameObject door5;
    private GameObject door6;
    private GameObject door7;

    public bool escaped;

    public int personnageSpeedWalk;
    public int personnageSpeedRun;
    private int personnageSpeed;
    public int jumpSpeed;
    private bool isGrounded;
    
    public AudioClip jump;
    public AudioClip land;
    public AudioSource sound;
    
    //mouvement de tete : sensi
    public static int sensibility;

    //life/attack/shield
    public static int life;
    public static int attack;
    public static int shield;

    //temps avant la regen du shield
    private static float shieldCooldown = 0f;

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
    public GameObject balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;
    public Transform BulletPos;

    public GameObject weapon;

    private Vector3 Shoot;

    //Animation
    private Animator animator;
    private bool run;

    //Canvas
    private Canvas_UI_Online can;

    public Text text;
    public Text text_msg;
    public Text text_sec;
    public Text text_infos;
    public Text text_pause;

    public GameObject quit;

    public RawImage healthBar;
    public RawImage shieldBar;

    public Image msg_img;

    //Layer (pour la detection)
    private static int layer = 8;

    #endregion



    #region Unity methods

    void Start () {

        door1 = GameObject.Find("door1");
        door2 = GameObject.Find("door2");
        door3 = GameObject.Find("door3");
        door4 = GameObject.Find("door4");
        door5 = GameObject.Find("door5");
        door6 = GameObject.Find("door6");
        door7 = GameObject.Find("door7");

        can = new Canvas_UI_Online(this, quit, text, text_msg, text_sec, text_infos, text_pause, healthBar, shieldBar, msg_img, GetComponent<Transform>(), door1, door2, door3, door4, door5, door6, door7);

        animator = GetComponent<Animator>();
        run = false;

        escaped = false;

        if (sensibility == 0)
        {
            sensibility = 5;
        }

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

        Shoot = new Vector3(0, 0, 1);

        //Layer Set
        this.gameObject.layer = layer;
        this.GetComponentInChildren<Collider>().gameObject.layer = layer;

    }
	
	void FixedUpdate () {

        if (isLocalPlayer)
        {
            can.Display(life, shield);
        }

        CheckDoors();

        if (!escaped && isLocalPlayer)
        {

            if (!cam.enabled)
                cam.enabled = true;
            if (!al.enabled)
                al.enabled = true;

            Moves();
            AnimPerso();

            if(Time.time > shieldCooldown)
            {
                ShieldRegeneration();
            }

            if (Time.time > nextFire)
            {
                isSurchauffe = false;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    fireRate = 0.36f;
                    nextFire = Time.time + fireRate;
                    CmdFire();
                    nbTirs = 0;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    if (nbTirs < 20)
                    {
                        fireRate = 0.1f;
                        nbTirs++;
                        nextFire = Time.time + fireRate;
                        CmdBurst_Fire();
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
            run = true;   
        }
        else
        {
            personnageSpeed = personnageSpeedWalk;
            run = false;
        }        

        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);

        
        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            Spine.transform.Rotate(0, 0, Input.GetAxisRaw("Mouse Y") * sensibility);
            Shoot.y = Input.GetAxisRaw("Mouse Y") * sensibility;
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

        if (BalleTir_offline.isSurchauffe)
        {
            animator.SetBool("isShooting", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);

            //anim["surchauffe"].speed = 0.7f;
            //anim.Play("surchauffe");
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if (run)
            {
                animator.SetBool("isShooting", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                //anim["Walk"].speed = 4f;
            }
            else
            {
                animator.SetBool("isShooting", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                //anim["Walk"].speed = 2f;
            }
        }
        else
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            //anim.Play("Idle");
        }



    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy_atk")
        {
            Damage();
        }
        else
        {
            isGrounded = true;
        }
        player.velocity = new Vector3(0, 0, 0);
        
    }

    [Command]
    public void CmdFire()
    {

        /*GameObject balle;

        i++;

        Quaternion qua = new Quaternion(0, 0, 0, 0); //GetComponentInParent<Rigidbody>().transform.rotation.x

        balle = Instantiate(balleCasting.gameObject, BulletPos.transform.position, qua);

        balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection( Shoot ) * ejectSpeed;
        balle.GetComponent<Rigidbody>().isKinematic = false;*/

        GameObject balle = blabla.CmdFire(balleCasting, BulletPos.transform.position, weapon);

        NetworkServer.Spawn(balle);            

        balle.name = "Bullet " + i;


        shoot.Play();
    }

    [Command]
    public void CmdBurst_Fire()
    {



        /*i++;
        balle = Instantiate(balleCasting.gameObject, BulletPos.transform.position, new Quaternion(BulletPos.transform.rotation.x,transform.rotation.y,BulletPos.transform.rotation.z,BulletPos.transform.rotation.w));
        balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * ejectSpeed;
        balle.GetComponent<Rigidbody>().isKinematic = false;*/

        GameObject balle = blabla.CmdBurst_Fire(balleCasting, BulletPos.transform.position, weapon);

        NetworkServer.Spawn(balle);

        balle.name = "Bullet " + i;

        shoot.Play();
    }

    #endregion


    [Command]
    public void CmdOpen_door(GameObject door, int nbMax)
    {
        if (transform.position.y <= nbMax)
        {
            door.transform.Translate(new Vector3(0, 0, 1));
        }
            
    }

    public bool FonctionNulleQuiRetrouneBool(GameObject door, int nbMax)
    {
        return door.transform.position.y > nbMax;
    }

    private void CheckDoors()
    {
        if (door1 == null || can.door1 == null)
        {
            door1 = GameObject.Find("door1");
            can.door1 = door1;
        }
        if (door2 == null || can.door2 == null)
        {
            door2 = GameObject.Find("door2");
            can.door2 = door2;
        }
        if (door3 == null || can.door3 == null)
        {
            door3 = GameObject.Find("door3");
            can.door3 = door3;
        }
        if (door4 == null || can.door4 == null)
        {
            door4 = GameObject.Find("door4");
            can.door4 = door4;
        }
        if (door5 == null || can.door5 == null)
        {
            door5 = GameObject.Find("door5");
            can.door5 = door5;
        }
        if (door6 == null || can.door6 == null)
        {
            door6 = GameObject.Find("door6");
            can.door6 = door6;
        }
        if (door7 == null || can.door7 == null)
        {
            door7 = GameObject.Find("door7");
            can.door7 = door7;
        }
    }

    private void Damage()
    {
        shieldCooldown = Time.time + 3;
        if(shield > 0)
        {
            shield -= 5;
            if (shield < 0)
            {
                shield = 0;
            }
        }
        else
        {
            life -= 5;
            if (life < 0)
            {
                life = 0;
            }
        }
    }

    private void ShieldRegeneration()
    {
        if (shield < 100)
        {
            shield++;
        }
    }

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