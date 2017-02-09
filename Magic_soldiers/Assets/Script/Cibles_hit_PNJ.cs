using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cibles_hit_PNJ : MonoBehaviour {

    public float nextFire;
    public float fireRate = 1f;

    private int nbRan;

    private ParticleSystem particules;

    private

    void Start()
    {
        particules = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            nbRan = (int)(Random.value * 2 + 1);

            if (nbRan == 1)
            {
                particules.Play();
            }
        }




    }

}
