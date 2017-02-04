using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cibles_hit : MonoBehaviour {

    private bool isHit;

	void Start () {
		
	}
	
	
	void Update () {
        if (isHit)
        {
            if (transform.eulerAngles.x > 280)
            {
                transform.Rotate(-300 * Time.deltaTime, 0, 0);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(-300 * Time.deltaTime, 0, 0);
        isHit = true;
    }
}
