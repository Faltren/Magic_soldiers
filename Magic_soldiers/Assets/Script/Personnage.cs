using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Personnage : MonoBehaviour {

    #region Attributes

    public int personnageSpeed;
    private int gravity = 10;

    //mouvement de tete : limite vers le haut et limite vers le bas et sensi
    public static int sensibility = 5;
    public static float limitMoveUp = 310f;
    public static float limitMoveDown = 250f;

    private Vector3 directionMove = Vector3.zero;
    private CharacterController player;
    public static Animation anim;

    #endregion



    #region Unity methods
    // Use this for initialization

        void Start () {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Moves();
        //AnimPerso();

	}
    #endregion


    #region otherMethods


    private void Moves()
    {

        directionMove.z = Input.GetAxis("Vertical"); //avant / arriere
        directionMove.x = Input.GetAxis("Horizontal"); //gauche / droite

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            personnageSpeed = 14;   
        }
        else
        {
            personnageSpeed = 7;
        }


        //deplacement
        directionMove = transform.TransformDirection(directionMove.x * personnageSpeed, directionMove.y, directionMove.z * personnageSpeed);
        directionMove *= Time.deltaTime;
        
        if (!player.isGrounded)
            directionMove.y -= gravity * Time.deltaTime;

        player.Move(directionMove);


        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);



    }

    private void AnimPerso()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            anim.Play("assault_combat_run");
        else
            anim.Stop();
    }


    #endregion


}
