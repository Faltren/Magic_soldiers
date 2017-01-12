using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armeMovement : MonoBehaviour {

    private int sensibility = Personnage.sensibility;
    private float limiteHaut = Personnage.limitMoveUp;
    private float limiteBas = Personnage.limitMoveDown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mouse();
	}

    private void Mouse()
    {
        if (transform.rotation.eulerAngles.z < limiteHaut && transform.rotation.eulerAngles.z > limiteBas)
            transform.Rotate(0, 0, Input.GetAxisRaw("Mouse Y") * sensibility);
        else if (transform.rotation.eulerAngles.z >= limiteHaut)
            transform.Rotate(0, 0, -0.3f);
        else if (transform.rotation.eulerAngles.z <= limiteBas)
            transform.Rotate(0, 0, 0.3f);
    }
}
