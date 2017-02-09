using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAtest : MonoBehaviour {
    private bool pattern;
    private float posZ0;
    private float posX0;
    private float posZ;
    private float posX;
    private int pos;
    public float speed;

    private float xPlayer;
    private float zPlayer;
    private int DetectRadius;
	// Use this for initialization
	void Start () {
        pattern = true;
        posX0 = transform.position.x;
        posZ0 = transform.position.z;
        posZ = 0;
        posX = -10;
        pos = 0;
        

        xPlayer = GameObject.Find("PlayerDummy").transform.position.x;
        zPlayer = GameObject.Find("PlayerDummy").transform.position.z;
        DetectRadius = 25;

    }

    // Update is called once per frame
    void Update() {

        DetectPlayer();

        if (pattern)
        {
            Pattern();
        }
        else
        {
            Attack();
        }
        transform.position = new Vector3(posX0 + posX, 0, posZ0 + posZ);
    }

    private void Pattern()
    {
        if (pos == 0)
        {
            if (posX < 10)
            {
                posX += Time.deltaTime*speed;
            }
            else
            {
                pos++;
            }
        }

        if (pos == 1)
        {
            if (posZ < 10)
            {
                posZ += Time.deltaTime * speed;
            }
            else
            {
                pos++;
            }
        }

        if (pos == 2)
        {
            if (posX > -10)
            {
                posX -= Time.deltaTime * speed;
            }
            else
            {
                pos++;
            }
        }
        if (pos == 3)
        {
            if (posZ > 0)
            {
                posZ -= Time.deltaTime * speed;
            }
            else
            {
                pos = 0;
            }
        }
    }

    private void Attack()
    {
        if(GameObject.Find("PlayerDummy").transform.position.x - 2 <= posX0 + posX)
        {
            posX -= Time.deltaTime * speed*2;
        }
        if(GameObject.Find("PlayerDummy").transform.position.x + 2 >= posX0 + posX)
        {
            posX += Time.deltaTime * speed*2;
        }
        if(GameObject.Find("PlayerDummy").transform.position.z - 2 <= posZ0 + posZ)
        {
            posZ -= Time.deltaTime * speed*2;
        }
        if (GameObject.Find("PlayerDummy").transform.position.z + 2 >= posZ0 + posZ)
        {
            posZ += Time.deltaTime * speed*2;
        }
    }

    private void DetectPlayer()
    {
        xPlayer = GameObject.Find("PlayerDummy").transform.position.x;
        zPlayer = GameObject.Find("PlayerDummy").transform.position.z;
        pattern = !((xPlayer - (posX0 + posX)) * (xPlayer - (posX0 + posX)) + (zPlayer - (posZ0 + posZ)) * (zPlayer - (posZ0 + posZ)) <= DetectRadius * DetectRadius);
    }



}
