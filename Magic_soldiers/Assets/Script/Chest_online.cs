using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chest_online : NetworkBehaviour {

    public bool HasCoin;

    private bool open;

    float number;

    private float xPlayer;
    private float zPlayer;

    private float posX0;
    private float posZ0;

    public float DetectRadius;

    private GameObject[] players;

	void Start () {

        open = false;

        posX0 = transform.position.x;
        posZ0 = transform.position.z;

        xPlayer = GameObject.Find("Perso(Clone)").transform.position.x;
        zPlayer = GameObject.Find("Perso(Clone)").transform.position.z;

        players = GameObject.FindGameObjectsWithTag("Player");

        number = 0;
    }
	

	void Update () {

        foreach (GameObject player in players)
        {
            xPlayer = player.transform.position.x;
            zPlayer = player.transform.position.z;


            number = (xPlayer - posX0) * (xPlayer - posX0) + (zPlayer - posZ0) * (zPlayer - posZ0);

            if (number <= DetectRadius * DetectRadius && !open)
            {
                open = true;
                if(HasCoin)
                    Bonus();
            }

            if (open)
            {
                CmdOpen();
            }
        }
               
    }

    [Command]
    private void CmdOpen()
    {
        if (transform.eulerAngles.x < 310 || transform.eulerAngles.x > 330)
        {
            transform.Rotate(new Vector3(-3, 0, 0));
        }
    }

    private void Bonus()
    {
        Personnage.life = 100;

        int n = Random.Range(0, 3);

        //attack / vitesse

        switch (n)
        {
            case 0:
                if (Personnage.attack <= 10)
                    Personnage.attack += 1;                          
                break;
            case 1:
                if (Personnage.personnageSpeedWalk <= 15)
                {
                    Personnage.personnageSpeedRun += 1;
                    Personnage.personnageSpeedWalk += 1;
                }
                break;
            case 2:
                if (Personnage.nbTirsMax <= 30)
                {
                    Personnage.nbTirsMax += 1;
                }
                break;

            default:
                break; 
        }

    }


}
