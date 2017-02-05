using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entrainActivate : MonoBehaviour {

    public static bool IsActivated = false;
    public static int nbHit;

    void Start () {
        nbHit = 0;
    }
	
	void Update () {

        if (Personnage.player.transform.position.x > -113 && Personnage.player.transform.position.x < 3.5)
        {
            if (Personnage.player.transform.position.z > -150 && Personnage.player.transform.position.z < -81)
            {
                IsActivated = true;
                
            }
            else
            {
                IsActivated = false;
            }
        }
        else
        {
            IsActivated = false;
        }
    }




}
