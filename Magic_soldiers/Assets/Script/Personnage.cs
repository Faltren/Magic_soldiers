using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour {

    #region Attributes

    private int personnageSpeed = 5;
    private Vector3 directionMove = Vector3.zero;
    private CharacterController player;

    #endregion



    #region Unity methods
    // Use this for initialization
    void Start () {

        player = GetComponent<CharacterController>();
        		
	}
	
	// Update is called once per frame
	void Update () {

        Moves();

	}
    #endregion





    #region otherMethods


    private void Moves()
    {
        directionMove.z = Input.GetAxis("Vertical"); //avant / arriere
        directionMove.x = Input.GetAxis("Horizontal"); //gauche / droite
        directionMove = transform.TransformDirection(directionMove * personnageSpeed * Time.deltaTime);
        player.Move(directionMove);

        
    }



    #endregion


}
