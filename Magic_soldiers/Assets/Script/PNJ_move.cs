using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_move : MonoBehaviour {

    private Animation anim;

    public float speed;
    public float speedAnim;

    private bool one;
    public int FirstX;
    public int FirstY;
    public int FirstZ;

    private bool two;
    public int SecX;
    public int SecY;
    public int SecZ;

    private bool three;
    public int ThirdX;
    public int ThirdY;
    public int ThirdZ;

    void Start () {
        anim = GetComponent<Animation>();
        one = true;
        two = false;
        three = false;
	}
	
	
	void Update () {

        anim["Run_Rifle_Foreward_legacy"].speed = speedAnim;
        anim.Play();

        if (one)
        {
            if (transform.position.x - 1 <= FirstX && transform.position.x + 1 >= FirstX && transform.position.y - 1 <= FirstY && transform.position.y + 1 >= FirstY && transform.position.z - 1 <= FirstZ && transform.position.z + 1 >= FirstZ)
            {
                one = false;
                two = true;
            }

           

            Move(FirstX, FirstY, FirstZ);

        }
        else if (two)
        {
            if (transform.position.x - 1 <= SecX && transform.position.x + 1 >= SecX && transform.position.y - 1 <= SecY && transform.position.y + 1 >= SecY && transform.position.z - 1 <= SecZ && transform.position.z + 1 >= SecZ)
            {
                two = false;
                three = true;
            }

            Move(SecX, SecY, SecZ);
        }
        else if (three)
        {
            if (transform.position.x - 1 <= ThirdX && transform.position.x + 1 >= ThirdX && transform.position.y - 1 <= ThirdY && transform.position.y + 1 >= ThirdY && transform.position.z - 1 <= ThirdZ && transform.position.z + 1 >= ThirdZ)
            {
                three = false;
                one = true;
            }

            Move(ThirdX, ThirdY, ThirdZ);
        }

    }

    private void Move(int X, int Y, int Z)
    {
        transform.LookAt(new Vector3 (X, Y, Z));
        transform.Translate(0, 0, speed * Time.deltaTime);
    }


}
