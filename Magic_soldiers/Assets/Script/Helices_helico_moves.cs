using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helices_helico_moves : MonoBehaviour {

    public int speed = 900;

	void Start () {
		
	}
	
	void Update () {
        transform.Rotate(0, 0, speed * Time.deltaTime);
	}
}
