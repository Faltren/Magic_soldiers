using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class IAtest : NetworkBehaviour {
    private bool pattern;

    private float posZ0;
    private float posX0;
    private float posY0;

    private float posZ;
    private float posX;
    private float posY;

    private int pos;

    public int life;

    private int rotX = 0;

    public float speed;
    public float modif1;
    public float modif2;
    public bool hasPattern;

    Animator animator;
    bool isWalking;
    bool isAttacking = false;

    private float xPlayer;
    private float zPlayer;
    private float yPlayer;

    private GameObject[] players;
    private GameObject detectedPlayer;

    public int DetectRadius;
    public int attackRange;

    private UnityEngine.UI.Text lifeBar;

    private Rigidbody enemy;

    private float currentTime;

    private bool isDead;

    public bool isRanged;

    //Tir pour ennemis à distance
    BalleTir blabla;
    public GameObject weapon;
    public Transform BulletPos;
    public GameObject balleCasting;
    private ParticleSystem shoot;
    private float shootCooldown = 0f;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {

        pattern = true;
        posX0 = transform.position.x;
        posZ0 = transform.position.z;
        posY0 = transform.position.y;

        posZ = 0;
        posX = -10;
        pos = 0;

        isWalking = hasPattern;

        lifeBar = transform.FindChild("Life_Bar").gameObject.GetComponent<UnityEngine.UI.Text>();

        LifeDisplay();

        /*xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;
        yPlayer = GameObject.Find("Perso").transform.position.y;*/
        //DetectRadius = 25;

        enemy = this.GetComponent<Rigidbody>();

        isDead = false;

        players = GameObject.FindGameObjectsWithTag("Player");

        detectedPlayer = null;

        blabla = new BalleTir();
    }

    // Update is called once per frame
    void Update() {


        if (life > 0)
        {
            

            if (players.Length >= 1)
            {
                transform.FindChild("Life_Bar").LookAt(players[0].transform);
            }
            DetectPlayer();

            if (pattern)
            {
                if (hasPattern)
                {
                    Pattern();
                    transform.position = new Vector3(posX0 + posX, this.transform.position.y, posZ0 + posZ);
                }
            }
            else
            {
                Attack();
            }

            if ((isWalking) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                animator.SetTrigger("Walk");
            }

        }
        else
        {
            if (!isDead)
            {
                currentTime = Time.time;
            }

            isDead = true;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                animator.StopPlayback();
                animator.SetTrigger("Death");
                enemy.isKinematic = true;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && (currentTime + 5 < Time.time))
            {
                animator.Stop();
                Destroy(this.gameObject);
            }
        }
    }

    private void Pattern()
    {

        if (pos == 0)
        {

            if (posX < modif1)
            {
                posX += Time.deltaTime*speed;
            }
            else
            {
                pos++;
                transform.Rotate(0, -90, 0);
            }
        }

        if (pos == 1)
        {


            if (posZ < modif2)
            {
                posZ += Time.deltaTime * speed;
            }
            else
            {
                pos++;
                transform.Rotate(0, -90, 0);
            }
        }

        if (pos == 2)
        {

            if (posX > -modif1)
            {
                posX -= Time.deltaTime * speed;
            }
            else
            {
                pos++;
                transform.Rotate(0, -90, 0);
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
                transform.Rotate(0, -90, 0);
            }
        }
    }

    private void Attack()
    {


        if ((!isAttacking) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            transform.Translate(0,0,Time.deltaTime * speed * 2);
            posX = transform.position.x - posX0;
            posZ = transform.position.z - posZ0;
        }

        if((xPlayer - (posX0 + posX)) * (xPlayer - (posX0 + posX)) + (zPlayer - (posZ0 + posZ)) * (zPlayer - (posZ0 + posZ)) <= attackRange)
        {
            isAttacking = true;
            isWalking = false;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
            }
            if (isRanged)
            {
                if (shootCooldown < Time.time)
                {
                    this.transform.LookAt(new Vector3(xPlayer, this.transform.position.y, zPlayer));
                    shootCooldown = Time.time + 1.5f;
                    CmdFire();
                }
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isAttacking = false;
            isWalking = true;
        }
        else
        {
            Vector3 playerPos = new Vector3(xPlayer, this.transform.position.y, zPlayer);
            transform.LookAt(playerPos);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            xPlayer = detectedPlayer.transform.position.x;
            zPlayer = detectedPlayer.transform.position.z;
        }
        
    }

    private void DetectPlayer()
    {
        posY = transform.position.y;

        foreach (GameObject player in players)
        {
            xPlayer = player.transform.position.x;
            yPlayer = player.transform.position.y;
            zPlayer = player.transform.position.z;

            Vector3 playVect = new Vector3(xPlayer, yPlayer + 1, zPlayer);
            Vector3 enemyVect = new Vector3((posX0 + posX), posY + 1, (posZ0 + posZ));

            

            if (pattern)
            {
                pattern = !((xPlayer - (posX0 + posX)) * (xPlayer - (posX0 + posX)) + (zPlayer - (posZ0 + posZ)) * (zPlayer - (posZ0 + posZ)) <= DetectRadius * DetectRadius);

                if (!pattern)
                {
                    //Vector3 playVect = new Vector3(xPlayer, yPlayer, zPlayer);
                    //Vector3 enemyVect = new Vector3((posX0 + posX), posY, (posZ0 + posZ));

                    float distance = (float)Math.Sqrt((xPlayer - posX0 - posX) * (xPlayer - posX0 - posX) + (yPlayer - posY0 - posY) * (yPlayer - posY0 - posY) + (zPlayer - posZ0 - posZ) * (zPlayer - posZ0 - posZ));

                    var layerMask = 1;

                    bool h = Physics.Linecast(enemyVect, playVect, layerMask);

                    pattern = h;

                    Debug.DrawLine(enemyVect, playVect, Color.yellow);

                    /*print(layerMask);
                    print(h);
                    print(distance);
                    Debug.DrawLine(enemyVect, playVect, Color.yellow);*/

                }
                if (!pattern)
                {
                    detectedPlayer = player;
                }
            }
        }
    }

    private void LifeDisplay()
    {
        lifeBar.text = "";
        for (int i = 0; i < life; i++)
        {  
            lifeBar.text += '-';
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        enemy.velocity = new Vector3(0,0,0);

        string s = "";
        string name = col.gameObject.name;
        int i = 0;

        while ((i < name.Length) && (s != "Bullet"))
        {
            s += name[i];
            i++;
        }

        if ((s == "Bullet") && (name.Length > 7))
        {
            int oldLife = life;
            life -= Personnage_offline.attack;
            if (life == oldLife)
            {
                life -= Personnage.attack;
            }

            if (life < 0)
            {
                life = 0;
            }

            LifeDisplay();
            Destroy(col.gameObject);
        }
    }

    [Command]
    public void CmdFire()
    {
        GameObject balle;

        Quaternion qua = new Quaternion(0, 0, 0, 0); //GetComponentInParent<Rigidbody>().transform.rotation.x

        balle = Instantiate(balleCasting, BulletPos.transform.position, qua);

        balle.GetComponent<Rigidbody>().velocity = weapon.transform.TransformDirection(Vector3.forward) * 50;
        balle.GetComponent<Rigidbody>().isKinematic = false;

        NetworkServer.Spawn(balle);

        balle.name = "Enemy_Bullet";
        balle.tag = "enemy_atk";

    }
}
