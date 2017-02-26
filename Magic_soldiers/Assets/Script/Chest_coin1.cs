using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_coin1 : MonoBehaviour {

    private float posY;

    private Object piece;

	void Start () {

        piece = GetComponent<Object>();
        posY = transform.position.y;
	}
	
	void Update () {
        if (Chest1.open)
        {
            if (transform.position.y < posY + 0.5f)
                transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0));

            if (Input.GetKeyUp(KeyCode.E))
                Destroy((piece as Transform).gameObject);


            transform.Rotate(new Vector3 (0,1.5f,0));
        }
	}
}
