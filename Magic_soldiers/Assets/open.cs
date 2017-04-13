using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class open : MonoBehaviour
{

    private float xPlayer;
    private float zPlayer;

    private float posX;
    private float posZ;

    private bool find;
    public static bool close;

    private int DetectRadius;

    private Text text;

    void Start()
    {
        posX = transform.position.x;
        posZ = transform.position.z;
        DetectRadius = 5;
        close = true;
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        DetectPlayer();

        if (!close)
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
        find = ((xPlayer - posX) * (xPlayer - posX) + (zPlayer - posZ) * (zPlayer - posZ) <= DetectRadius * DetectRadius);

        if (find)
        {
            if (!Chest.open)
                text.text = "Appuyez sur E pour interagir";
            else
                text.text = "";

            if (Input.GetKey(KeyCode.E))
                close = false;

        }
    }
}
