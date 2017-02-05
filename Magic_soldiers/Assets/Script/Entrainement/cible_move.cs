using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cible_move : MonoBehaviour {

    public int maxX;
    public int minX;

    public int maxY;
    public int minY;

    public int maxZ;
    public int minZ;

    public bool X;
    public bool Y;
    public bool Z;

    public int speed;

    private bool left;
    private bool right;

    void Start () {

        right = true;
        left = false;  
	}
	
	void Update () {

        if (entrainActivate.IsActivated)
        {
            if (transform.position.x >= maxX && X)
            {
                left = true;
                right = false;
                speed = -speed;
            }
            if (transform.position.x <= minX && X)
            {
                left = false;
                right = true;
                speed = -speed;
            }

            if (transform.position.y >= maxY && Y)
            {
                left = true;
                right = false;
                speed = -speed;
            }
            if (transform.position.y <= minY && Y)
            {
                left = false;
                right = true;
                speed = -speed;
            }

            if (transform.position.z >= maxZ && Z)
            {
                left = true;
                right = false;
                speed = -speed;
            }
            if (transform.position.z <= minZ && Z)
            {
                left = false;
                right = true;
                speed = -speed;
            }

            if (transform.position.x < maxX && right && X)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x > minX && left && X)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }

            if (transform.position.y < maxY && right && Y)
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
            else if (transform.position.y > minY && left && Y)
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }

            if (transform.position.z < maxZ && right && Z)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.z > minZ && left && Z)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }

    }
}
