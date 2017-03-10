using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PersoHautBas : NetworkBehaviour
{

    private int sensi = Personnage.sensibility;
    private float souris;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*if (isLocalPlayer)
        {
            if (!Canvas_UI.isPaused)
            {
                if (Input.GetAxisRaw("Mouse Y") != 0)
                {
                    souris = Input.GetAxisRaw("Mouse Y");
                    transform.Rotate(0, 0, souris * sensi);
                }

                if (transform.eulerAngles.z > 330)
                {
                    transform.eulerAngles = new Vector3(
                           transform.eulerAngles.x,
                           transform.eulerAngles.y,
                           330);
                }

                if (transform.eulerAngles.z < 200)
                {
                    transform.eulerAngles = new Vector3(
                           transform.eulerAngles.x,
                           transform.eulerAngles.y,
                           200);
                }
            }
        }*/


    }




}
