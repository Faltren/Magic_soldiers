using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Boss2 : NetworkBehaviour {

    bool attackTriggered;

    private float posZ0;
    private float posX0;
    private float posY0;

    private float posZ;
    private float posX;
    private float posY;

    public int life;
    public int detectRadius;

    private string transition;

    private UnityEngine.UI.Text lifeBar;

    private bool isDead;

    private int fired;

    private Rigidbody enemy;
    Animator animator;


    public GameObject weapon;
    public Transform BulletPos;
    public GameObject balleCasting;
    private ParticleSystem shoot;
    private float shootCooldown;
    private float summonCooldown;
    private float targetCooldown;

    private int target;

    private GameObject[] players;
    private GameObject attackedPlayer;

    float currentTime;

    private float xPlayer;
    private float zPlayer;
    private float yPlayer;

    public GameObject summon1;
    public GameObject summon2;
    public GameObject summon3;
    public GameObject MonsterCast;

    private GameObject tPlayer;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {

        attackTriggered = false;

        target = 0;

        posX0 = transform.position.x;
        posZ0 = transform.position.z;
        posY0 = transform.position.y;

        lifeBar = transform.FindChild("Life_Bar").gameObject.GetComponent<UnityEngine.UI.Text>();

        enemy = this.GetComponent<Rigidbody>();

        players = GameObject.FindGameObjectsWithTag("Player");

        attackedPlayer = null;

        transition = "";

        fired = 0;
        shootCooldown = 0;
        summonCooldown = 0;
        targetCooldown = 0;

        players = GameObject.FindGameObjectsWithTag("Player");

        LifeDisplay();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (life > 0)
        {
            lifeBar.transform.LookAt(new Vector3(players[0].transform.position.x, players[0].transform.position.y, players[0].transform.position.z));
            if (attackTriggered)
            {
                if (targetCooldown < Time.time)
                {
                    tPlayer = players[target];
                    targetCooldown = Time.time + 8;
                    target++;
                    if (target >= players.Length)
                        target = 0;

                }

                xPlayer = tPlayer.transform.position.x;
                yPlayer = this.transform.position.y;
                zPlayer = tPlayer.transform.position.z;

                Vector3 pPos = new Vector3(xPlayer, yPlayer, zPlayer);

                this.transform.LookAt(pPos);
                
                if (summonCooldown < Time.time)
                {
                    CmdSummon();
                    summonCooldown = Time.time + 12.5f;
                    transition = "Summon";
                }
                else
                {
                    if (shootCooldown < Time.time)
                    {
                        if (fired < 3)
                        {
                            shootCooldown = Time.time + 0.5f;
                            CmdFire();
                            fired++;
                            transition = "Attack";
                        }
                        else
                        {
                            fired = 0;
                            shootCooldown = Time.time + 2;
                        }
                    }
                }
            }
            else
            {
                DetectPlayer();
            }
            Animation();
        }
        else
        {
            if (!isDead)
            {
                currentTime = Time.time;
            }

            isDead = true;

            enemy.isKinematic = true;
            transition = "Death";
            Animation();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("SumonnerDeath") && (currentTime + 5 < Time.time))
            {
                animator.Stop();
                Destroy(this.gameObject);
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
        enemy.velocity = new Vector3(0, 0, 0);

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
            life -= (Personnage_offline.attack);
            if (life == oldLife)
            {
                life -= 1 + (Personnage.attack % 2);
            }

            if (life < 0)
            {
                life = 0;
            }

            LifeDisplay();
            Destroy(col.gameObject);
        }
    }

    private void DetectPlayer()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        foreach (GameObject player in players)
        {
            xPlayer = player.transform.position.x;
            yPlayer = player.transform.position.y;
            zPlayer = player.transform.position.z;

            Vector3 playVect = new Vector3(xPlayer, yPlayer + 1, zPlayer);
            Vector3 enemyVect = new Vector3(posX, posY + 1, posZ);

            if (!attackTriggered)
            {
                attackTriggered = ((xPlayer - (posX)) * (xPlayer - (posX)) + (zPlayer - (posZ)) * (zPlayer - (posZ)) <= detectRadius * detectRadius);
                if (attackTriggered)
                {
                    var layerMask = 1;

                    bool h = Physics.Linecast(enemyVect, playVect, layerMask);

                    attackTriggered = !h;
                }
            }
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

    [Command]
    public void CmdSummon()
    {
        GameObject skeleton1;
        GameObject skeleton2;
        GameObject skeleton3;

        Quaternion qua = new Quaternion(0, 0, 0, 0);

        skeleton1 = Instantiate(MonsterCast, summon1.transform.position, qua);
        skeleton2 = Instantiate(MonsterCast, summon2.transform.position, qua);
        skeleton3 = Instantiate(MonsterCast, summon3.transform.position, qua);

        NetworkServer.Spawn(skeleton1);
        NetworkServer.Spawn(skeleton2);
        NetworkServer.Spawn(skeleton3);
    }

    private void Animation()
    {
        if (transition != "")
        {
            animator.SetTrigger(transition);
            if (transition == "Punch")
            {
                transition = "";
            }
        }
    }
}
