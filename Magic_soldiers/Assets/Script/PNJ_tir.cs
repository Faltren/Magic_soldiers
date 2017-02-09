using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PNJ_tir : MonoBehaviour {

    private Animation PNJanim;
    public bool burst;
    public float nextFire;
    public float fireRate = 1f;

    void Start () {
        PNJanim = GetComponent<Animation>();
    }
	
	void Update () {

        if (Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;

            if (burst)
            {
                PNJanim.Play("assault_combat_shoot_burst");
            }
            else
            {
                PNJanim.Play("assault_combat_shoot");
            }


        }


    }
}
