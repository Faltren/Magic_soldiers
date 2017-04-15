using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Open_door : MonoBehaviour {

    private bool canOpen;
    private float nbY;

    public Open_door()
    {
        canOpen = false;
        nbY = 0;
    }

    public void CanOpen(float nbY)
    {
        canOpen = true;
        this.nbY = nbY;
    }

    //[Command]
    public void CmdOpen()
    {
        print(gameObject);

        if (canOpen)
        {
            nbY = transform.position.y;

            if(transform.position.y <= nbY)
            {
                transform.Translate(new Vector3(0, 2, 0));
            }

        }
    }

}
