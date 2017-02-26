using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectifs_particules : MonoBehaviour {

    private ParticleSystem particules;
	
	void Start () {

        particules = GetComponent<ParticleSystem>();
	}
	
	
	void Update () {

        if (particules.name == "Obj_particules" + Canvas_UI.compteur.ToString())
            particules.Play();
        else
            particules.Stop();

	}
}
