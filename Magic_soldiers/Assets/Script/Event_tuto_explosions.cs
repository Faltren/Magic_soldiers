using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Event_tuto_explosions : MonoBehaviour {

	private ParticleSystem particules;
    public AudioClip fire_sound;
    private AudioSource sound;

	void Start () {

        particules = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
	}
	
	
	void Update () {

        if (EditorSceneManager.GetActiveScene().name == "Tuto")
        {

            if (Personnage.player.transform.position.z < -528 && Personnage.player.transform.position.z > -900)
            {
                if (Personnage.player.transform.position.x < 635 && Personnage.player.transform.position.z > 370)
                {
                    particules.Stop();
                }
            }
            else if (Canvas_UI.compteur >= 8)
            {
                particules.Play();
                sound.PlayOneShot(fire_sound);
            }
            else
            {
                particules.Stop();
            }


        }

            

	}
}
