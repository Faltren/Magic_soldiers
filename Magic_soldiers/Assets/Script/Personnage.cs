using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour {

    #region Attributes




    //mouvement de tete : limite vers le haut et limite vers le bas et sensi
    public static int sensibility = 5;
    public static float limitMoveUp = 310f;
    public static float limitMoveDown = 250f;

    private Vector3 directionMove = Vector3.zero;
    
    #endregion



    #region Unity methods
    // Use this for initialization
    void Start () {
             		
	}
	
	// Update is called once per frame
	void Update () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Moves();

	}
    #endregion





    #region otherMethods


    private void Moves()
    {
        /*directionMove.z = Input.GetAxis("Vertical"); //avant / arriere
        directionMove.x = Input.GetAxis("Horizontal"); //gauche / droite

        //deplacement
        directionMove = transform.TransformDirection(directionMove.x * personnageSpeed , directionMove.y, directionMove.z * personnageSpeed);

        //saut
        directionMove *= jumpSpeed;
        if (Input.GetButton("Jump") && player.isGrounded)
        {
            directionMove.y = jump;
        }

        directionMove.y -= gravity * Time.deltaTime;
        player.Move(directionMove);

        directionMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        directionMove = transform.TransformDirection(directionMove);
        directionMove *= personnageSpeed;

        if (Input.GetButton("Jump") && directionMove.y < 10)
        {
            nbJump++;
            directionMove.y = jumpSpeed;
        }

        if (player.isGrounded && nbJump > 0)
            nbJump = 0;

        directionMove.y -= gravity * Time.deltaTime;
        player.Move(directionMove * Time.deltaTime);*/





        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);

    }



    #endregion


}
