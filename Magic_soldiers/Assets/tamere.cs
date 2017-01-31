using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamere : MonoBehaviour {

    private GameObject blabla;

	// Use this for initialization
	void Start () {
        blabla = GetComponent<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.B))
        {
            print(blabla.name);
            //Destroy(gameObject.name);
            
        }
            
	}
}
