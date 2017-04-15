using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particules_online : MonoBehaviour {

    private int compteur;

    public Particules_online(int startCompteur)
    {
        compteur = startCompteur;
    }

    public void Activation()
    {
        if (GetComponent<ParticleSystem>().name == "Obj_particules" + compteur.ToString())
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }



}
