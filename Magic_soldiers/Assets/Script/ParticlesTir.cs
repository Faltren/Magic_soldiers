using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesTir : MonoBehaviour {

    private ParticleSystem particules;

    private bool playing = false;
    private float lifeParticles = 3.0f;

	// Use this for initialization
	void Start () {
        particules = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > BalleTir.nextFire && playing)
        {
            playing = true;
            particules.Play();
            lifeParticles = Time.time + 3.0f;
        }
        else if (particules.isPlaying && Time.time > lifeParticles)
        {
            playing = false;
        }
        else if(!playing)
        {
            particules.Stop();
        }

    }
}
