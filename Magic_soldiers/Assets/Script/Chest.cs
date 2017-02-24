using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private float xPlayer;
    private float zPlayer;

    private float posX;
    private float posZ;

    private bool find;
    public static bool open;

    private int DetectRadius;

	void Start () {
        posX = transform.position.x;
        posZ = transform.position.z;
        DetectRadius = 5;
        open = false;
    }
	
	void Update () {
        DetectPlayer();

        if (open)
        {
            if (transform.eulerAngles.x < 310 || transform.eulerAngles.x > 330)
            {
                transform.Rotate(new Vector3(-3, 0, 0));
            }
        }
	}


    private void DetectPlayer()
    {
        xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;
        find = ((xPlayer - posX) * (xPlayer - posX) + (zPlayer -  posZ) * (zPlayer -  posZ) <= DetectRadius * DetectRadius);

        if (find)
        {
            if (Input.GetKey(KeyCode.E))
            {
                open = true;
            }
            
        }
    }


}
