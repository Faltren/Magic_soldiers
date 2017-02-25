using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_coin : MonoBehaviour {

    private float posY;

	void Start () {

        posY = transform.position.y;

	}
	
	void Update () {
        if (Chest.open)
        {
            if (transform.position.y < posY + 0.5f)
            {
                transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0));
            }

            transform.Rotate(new Vector3 (0,1.5f,0));
        }
	}
}
