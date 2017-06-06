using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Chest_online : NetworkBehaviour {

    private bool open;

    float number;

    private float xPlayer;
    private float zPlayer;

    private float posX0;
    private float posZ0;

    public float DetectRadius;

	void Start () {

        open = false;

        posX0 = transform.position.x;
        posZ0 = transform.position.z;

        xPlayer = GameObject.Find("Perso(Clone)").transform.position.x;
        zPlayer = GameObject.Find("Perso(Clone)").transform.position.z;

        number = 0;
    }
	

	void Update () {

        xPlayer = GameObject.Find("Perso(Clone)").transform.position.x;
        zPlayer = GameObject.Find("Perso(Clone)").transform.position.z;

        number = (xPlayer - posX0) * (xPlayer - posX0) + (zPlayer - posZ0) * (zPlayer - posZ0);

        if (number <= DetectRadius * DetectRadius && !open)
        {
            open = true;
        }

        if (open)
        {
            CmdOpen();
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

}
