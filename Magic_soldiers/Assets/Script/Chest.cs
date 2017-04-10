using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    private float xPlayer;
    private float zPlayer;

    private float posX;
    private float posZ;

    private bool find;
    public static bool open;

    private int DetectRadius;

    private Text text;

	void Start () {
        posX = transform.position.x;
        posZ = transform.position.z;
        DetectRadius = 5;
        open = false;
        text = GetComponentInChildren<Text>();
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
        xPlayer = GameObject.Find("Perso(Clone)").transform.position.x;
        zPlayer = GameObject.Find("Perso(Clone)").transform.position.z;
        find = ((xPlayer - posX) * (xPlayer - posX) + (zPlayer - posZ) * (zPlayer - posZ) <= DetectRadius * DetectRadius);

        if (find)
        {
            if (!Chest.open)
                text.text = "Appuyez sur E pour interagir";
            else
                text.text = "";

            if (Input.GetKey(KeyCode.E))
                open = true;

        }
    }


}
