using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helico_moves : MonoBehaviour {


    public int speed;
    public int angle;

    /*
    private bool un;
    public int unX;
    public int unY;
    public int unZ;

    private bool deux;
    public int deuxX;
    public int deuxY;
    public int deuxZ;

    private bool trois;
    public int troisX;
    public int troisY;
    public int troisZ;

    private bool quatre;
    public int quatreX;
    public int quatreY;
    public int quatreZ;*/

    void Start () {
        
	}
	
	
	void Update () {


        transform.Rotate(0, angle * Time.deltaTime, 0);
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        /*
        if(un)
        {
            if (transform.position.x - 1 <= unX && transform.position.x + 1 >= unX && transform.position.y - 1 <= unY && transform.position.y + 1 >= unY && transform.position.z - 1 <= unZ && transform.position.z + 1 >= unZ)
            {
                un = false;
                deux = true;
            }

            Move(unX, unY, unZ);
        }
        else if (deux)
        {
            if (transform.position.x - 1 <= deuxX && transform.position.x + 1 >= deuxX && transform.position.y - 1 <= deuxY && transform.position.y + 1 >= deuxY && transform.position.z - 1 <= deuxZ && transform.position.z + 1 >= deuxZ)
            {
                deux = false;
                trois = true;
            }

            if (transform.eulerAngles.z < 90)
            {
                transform.Rotate(0, 10 * Time.deltaTime, 0);
            }

            Move(deuxX, deuxY, deuxZ);
        }
        else if (trois)
        {
            if (transform.position.x - 1 <= troisX && transform.position.x + 1 >= troisX && transform.position.y - 1 <= troisY && transform.position.y + 1 >= troisY && transform.position.z - 1 <= troisZ && transform.position.z + 1 >= troisZ)
            {
                trois = false;
                quatre = true;
            }

            Move(troisX, troisY, troisZ);
        }
        else if (quatre)
        {
            if (transform.position.x - 1 <= quatreX && transform.position.x + 1 >= quatreX && transform.position.y - 1 <= quatreY && transform.position.y + 1 >= quatreY && transform.position.z - 1 <= quatreZ && transform.position.z + 1 >= quatreZ)
            {
                quatre = false;
                un = true;
            }

            Move(quatreX, quatreY, quatreZ);
        }*/



    }



    private void Move(int X, int Y, int Z)
    {
        if (X < transform.position.x)
        {
            transform.Translate(-speed * Time.deltaTime,0,0);
        } else if (X > transform.position.x)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Y < transform.position.y)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (Y > transform.position.y)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }

        if (Z < transform.position.z)
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (Z > transform.position.z)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }


    }


}
