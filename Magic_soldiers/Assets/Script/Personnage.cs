using System.Collections;
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
    
    public static Rigidbody player;
    public static Animation anim;


    #endregion



    #region Unity methods

        void Start () {

        life = 100;
        shield = 100;
        attack = 5;

        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        sound = GetComponent<AudioSource>();
        

        isGrounded = false;
        attack = 5;
    }
	
	void FixedUpdate () {

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        if (isLocalPlayer)
        {
            Moves();
            AnimPerso();
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