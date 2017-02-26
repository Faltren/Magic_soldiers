using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_texte : MonoBehaviour {

    private Text PNJtext;
    private Canvas can;

    private Vector3 target;

    private float xPlayer;
    private float zPlayer;

    private float posX;
    private float posZ;

    public string message;
    private float DetectRadius;
    private bool find;

    public bool needInteract;

    void Start () {

        DetectRadius = 10f;
        find = false;

        posX = transform.position.x;
        posZ = transform.position.z;

        PNJtext = GetComponentInChildren<Text>();
        can = GetComponent<Canvas>();

    }
	
	
	void Update () {

        if (DetectPlayer())
        {

            target = new Vector3(Personnage.player.transform.position.x, this.transform.position.y, Personnage.player.transform.position.z);
            can.transform.LookAt(target);

            if (!needInteract)
            {
                PNJtext.text = message;
            }
            else
            {
                if (Input.GetKey(KeyCode.E))
                {
                    PNJtext.text = message;
                }
                else
                {
                    PNJtext.text = "Appuyez sur E pour interagir";
                }
                    
            }

        }
        else
        {
            PNJtext.text = "";
        }

	}

    private bool DetectPlayer()
    {
        xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;
        find = ((xPlayer - posX) * (xPlayer - posX) + (zPlayer - posZ) * (zPlayer - posZ) <= DetectRadius * DetectRadius);

        return find;
     }




}
