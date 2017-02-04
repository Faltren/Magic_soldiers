using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helices_helico_moves : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        transform.Rotate(0, 0, 900 * Time.deltaTime);
	}
}
