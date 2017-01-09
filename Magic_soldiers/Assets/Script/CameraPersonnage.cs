using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPersonnage : MonoBehaviour {

    #region Attributes

    private Camera camera;
    private int sensibility = Personnage.sensibility;

    #endregion

    #region Unity methods

    // Use this for initialization
    void Start () {

        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        Moves();
		
	}
    #endregion


    #region otherMethods

    private void Moves()
    {

        transform.Rotate(Input.GetAxisRaw("Mouse Y") * -sensibility, 0, 0);
    }

    #endregion
}
