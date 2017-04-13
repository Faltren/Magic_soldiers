using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BalleTir : MonoBehaviour
{

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i; //name Compteur
    private int nbTirs;

    private Vector3 trans;

    public static bool isSurchauffe;

    public GameObject balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;

    public NetworkIdentity playerId;

    void Start () {
        isSurchauffe = false;
        shoot = GetComponentInChildren<ParticleSystem>();
        nbTirs = 0;
    }

	void Update () {

        /*if (playerId.isLocalPlayer)
        {
            if (Time.time > nextFire)
            {
                isSurchauffe = false;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    CmdFire();
                    nbTirs = 0;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    if (nbTirs < 20)
                    {
                        Burst_Fire();
                    }
                    else
                    {
                        isSurchauffe = true;
                        surchauffe.Play();
                        nbTirs = 0;
                        nextFire = Time.time + 2.5f;
                        fireRate = 0.36f;
                    }
                }

            }
        }*/
            
    }

    public GameObject CmdFire(GameObject balleCasting, Vector3 position, GameObject weapon)
    {
        
        GameObject balle;

        Quaternion qua = new Quaternion(0, 0, 0, 0); //GetComponentInParent<Rigidbody>().transform.rotation.x

        balle = Instantiate(balleCasting, position, qua);

        balle.GetComponent<Rigidbody>().velocity = weapon.transform.TransformDirection(Vector3.right) * 50;
        balle.GetComponent<Rigidbody>().isKinematic = false;

        return balle;
    }


    public GameObject CmdBurst_Fire(GameObject balleCasting, Vector3 position, GameObject weapon)
    {
        GameObject balle;

        Quaternion qua = new Quaternion(0, 0, 0, 0); //GetComponentInParent<Rigidbody>().transform.rotation.x

        balle = Instantiate(balleCasting, position, qua);

        balle.GetComponent<Rigidbody>().velocity = weapon.transform.TransformDirection(Vector3.right) * 50;
        balle.GetComponent<Rigidbody>().isKinematic = false;

        return balle;
    }
}








