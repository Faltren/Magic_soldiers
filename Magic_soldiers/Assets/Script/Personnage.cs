﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Personnage : MonoBehaviour {

    #region Attributes

    public int personnageSpeed;
    public int jumpSpeed;
    private bool isGrounded;

    //private int gravity = 10;

    //mouvement de tete : limite vers le haut et limite vers le bas et sensi
    public static int sensibility = 5;
    public static float limitMoveUp = 310f;
    public static float limitMoveDown = 250f;

    private Vector3 directionMove = Vector3.zero;
    //public static CharacterController player;
    public static Rigidbody player;
    public static Animation anim;


    #endregion



    #region Unity methods
    // Use this for initialization

        void Start () {
        //player = GetComponent<CharacterController>();
        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        isGrounded = false;
        player.freezeRotation = true;
        //directionMove.x = transform.position.x;
        //directionMove.z = transform.position.z;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
        Moves();
        //AnimPerso();

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
            isGrounded = false;
        }
       
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            personnageSpeed = 14;   
        }
        else
        {
            personnageSpeed = 7;
        }        

        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);

        //debug des forces
        player.velocity = new Vector3(0, player.velocity.y, 0);

    }

    private void AnimPerso()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            anim.Play("assault_combat_run");
        else
            anim.Stop();

        
    }



    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        player.velocity = new Vector3(0, 0, 0);
    }

    #endregion


}





//directionMove.z = directionMove.z + Input.GetAxis("Vertical") * personnageSpeed * Time.deltaTime; //avant / arriere
//directionMove.x = directionMove.x + Input.GetAxis("Horizontal") * personnageSpeed * Time.deltaTime; //gauche / droite


//deplacement
//directionMove = transform.TransformDirection(directionMove.x * personnageSpeed, directionMove.y, directionMove.z * personnageSpeed);
//directionMove *= Time.deltaTime;

/*if (!player.isGrounded)
    directionMove.y -= gravity * Time.deltaTime;*/

//player.Move(directionMove);