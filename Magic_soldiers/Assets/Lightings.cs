using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightings : MonoBehaviour
{

    public GameObject MainLight;
	// Use this for initialization
	void Start ()
    {
        MainLight.GetComponent<Light>().intensity = 0.1f;		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MainLight.GetComponent<Light>().intensity = 0.1f;
	}
}
