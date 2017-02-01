using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balle_explode_particles : MonoBehaviour {

    private ParticleSystem particules;

	// Use this for initialization
	void Start () {

        particules = GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {

        particules.Play();
        
	}
}
