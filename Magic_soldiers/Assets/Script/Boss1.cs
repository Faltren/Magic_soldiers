using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Boss1 : NetworkBehaviour {

    bool attackTriggered;

    private float posZ0;
    private float posX0;
    private float posY0;

    private float posZ;
    private float posX;
    private float posY;

    public int life;

    public float speed;

    Animator animator;

    private float xPlayer;
    private float zPlayer;
    private float yPlayer;

    private GameObject[] players;
    private GameObject attackedPlayer;

    public int detectRadius;
    public int attackRange;

    private UnityEngine.UI.Text lifeBar;

    private bool isDead;

    private string transition;

    public GameObject weapon;
    public Transform BulletPos;
    public GameObject balleCasting;
    private ParticleSystem shoot;
    private float shootCooldown = 0f;

    private int fired;

    private Rigidbody enemy;

    private int attackNb;

    float currentTime;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start() {

        attackTriggered = false;

        posX0 = transform.position.x;
        posZ0 = transform.position.z;
        posY0 = transform.position.y;

        lifeBar = transform.FindChild("Life_Bar").gameObject.GetComponent<UnityEngine.UI.Text>();

        enemy = this.GetComponent<Rigidbody>();

        players = GameObject.FindGameObjectsWithTag("Player");

        attackedPlayer = null;

        transition = "";
        attackNb = 0;

        fired = 0;

        LifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 0)
        {
            lifeBar.transform.LookAt(new Vector3(players[0].transform.position.x, players[0].transform.position.y, players[0].transform.position.z));
            if (!attackTriggered)
            {
                if (players.Length > 1)
                {
                    transform.FindChild("Life_Bar").LookAt(players[0].transform);
                }

                if (attackNb == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("CloseAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAttack"))
                {

                    System.Random rand = new System.Random();

                    attackedPlayer = players[rand.Next(0, players.Length)];

                    attackNb = rand.Next(1, 3);
                    print(attackNb);
                }
                Attack(attackNb);
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && (currentTime + 5 < Time.time))
            {
                animator.Stop();
                Destroy(this.gameObject);
            }
        }

    }

    private void Attack(int n)
    {
        posX = this.transform.position.x;
        posZ = this.transform.position.z;
        posY = this.transform.position.y;

        xPlayer = attackedPlayer.transform.position.x;
        yPlayer = attackedPlayer.transform.position.y;
        zPlayer = attackedPlayer.transform.position.z;

        Vector3 pPos = new Vector3(xPlayer, yPlayer, zPlayer);

        if (n == 1)
        {
            this.transform.LookAt(pPos);

            if ((xPlayer - (posX)) * (xPlayer - (posX)) + (zPlayer - (posZ)) * (zPlayer - (posZ)) <= 35)
            {
                transition = "Punch";
                attackNb = 0;
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("CloseAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAttack"))
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        transform.Translate(Vector3.forward * Time.deltaTime * speed * 2);
                    }
                    transition = "Rush";
                }

            }
        }
        if (n == 2)
        {
            if (fired < 3)
            {
                transition = "Shoot";
                if (shootCooldown < Time.time)
                {
                    print("shoot");
                    transition = "Shoot";
                    this.transform.LookAt(new Vector3(xPlayer, this.transform.position.y, zPlayer));
                    shootCooldown = Time.time + 0.5f;
                    CmdFire();
                    fired++;
                }
            }
            else
            {
                attackNb = 0;
                fired = 0;
            }
        }
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
                life -= (Personnage.attack);
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
}
