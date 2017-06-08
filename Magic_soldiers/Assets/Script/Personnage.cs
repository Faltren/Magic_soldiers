using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Personnage : NetworkBehaviour
{

    #region Attributes

    private float alpha;
    public static float alphamax = 0f;
    private bool bloodUp;

    private GameObject door1;
    private GameObject door2;
    private GameObject door3;
    private GameObject door4;
    private GameObject door5;
    private GameObject door6;
    private GameObject door7;
    private GameObject door8;
    private GameObject door9;
    private GameObject door10;
    private GameObject door11;
    private GameObject door12;
    private GameObject door13;
    private GameObject door14;
    private GameObject door15;


    public bool escaped;

    public static int personnageSpeedWalk = 10;
    public static int personnageSpeedRun = 20;
    private int personnageSpeed;
    public int jumpSpeed;
    private bool isGrounded;

    public AudioClip jump;
    public AudioClip land;
    public AudioSource sound;

    //mouvement de tete : sensi
    public static int sensibility;

    //life/attack/shield
    public static int life;
    public static int attack;
    public static int shield;

    public static int damageTaken = 5;
    //temps avant la regen du shield
    private static float shieldCooldown = 0f;

    private Rigidbody player;
    public GameObject Spine;
    public static Animation anim;

    private Camera cam;
    private AudioListener al;

    //Tir

    public Image surchauffe_img;

    public int ejectSpeed = 50; //etait a 20
    private float fireRate = 0.36f;
    public static float nextFire = 0.0f;
    private int i; //name Compteur
    private int nbTirs;

    public static int nbTirsMax = 20;

    private Vector3 trans;

    public static bool isSurchauffe;
    private BalleTir blabla;
    public GameObject balleCasting;
    private ParticleSystem shoot;
    public ParticleSystem surchauffe;
    public Transform BulletPos;

    public GameObject weapon;

    private Vector3 Shoot;

    //Animation
    private Animator animator;
    private bool run;

    //Canvas
    private Canvas_UI_Online can;

    public Text text;
    public Text text_msg;
    public Text text_sec;
    public Text text_infos;
    public Text text_pause;

    public GameObject quit;

    public RawImage healthBar;
    public RawImage shieldBar;

    public Image blood;
    private Color colBlood;

    //public RawImage healthBar;
    //public RawImage shieldBar;

    public Image msg_img;

    //Layer (pour la detection)
    private static int layer = 8;

    #endregion



    #region Unity methods

    void Start()
    {

        AudioListener.volume = Menu.volume;

        door1 = GameObject.Find("door1");
        door2 = GameObject.Find("door2");
        door3 = GameObject.Find("door3");
        door4 = GameObject.Find("door4");
        door5 = GameObject.Find("door5");
        door6 = GameObject.Find("door6");
        door7 = GameObject.Find("door7");
        door8 = GameObject.Find("door8");
        door9 = GameObject.Find("door9");
        door10 = GameObject.Find("door10");
        door11 = GameObject.Find("door11");
        door12 = GameObject.Find("door12");
        door13 = GameObject.Find("door13");
        door14 = GameObject.Find("door14");
        door15 = GameObject.Find("door15");


        can = new Canvas_UI_Online(this, quit, text, text_msg, text_sec, text_infos, text_pause, healthBar, shieldBar, msg_img, GetComponent<Transform>(), door1, door2, door3, door4, door5, door6, door7, door8, door9, door10, door11, door12, door13, door14, door15);

        alpha = 0;
        colBlood = blood.color;
        colBlood.a = alpha;
        blood.color = colBlood;

        bloodUp = false;

        animator = GetComponent<Animator>();
        run = false;

        escaped = false;

        if (sensibility == 0)
        {
            sensibility = 5;
        }

        blabla = new BalleTir();

        life = 100;
        shield = 100;
        attack = 2;

        player = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        sound = GetComponent<AudioSource>();

        cam = GetComponentInChildren<Camera>();
        al = GetComponentInChildren<AudioListener>();

        cam.transform.SetParent(Spine.transform);

        isGrounded = false;

        isSurchauffe = false;
        shoot = GetComponentInChildren<ParticleSystem>();
        nbTirs = 0;

        surchauffe_img.rectTransform.sizeDelta = new Vector2(nbTirs * (197f / nbTirsMax), 28); //20 tirs = 197 => 1 = 9.85

        Shoot = new Vector3(0, 0, 1);

        //Layer Set
        this.gameObject.layer = layer;
        this.GetComponentInChildren<Collider>().gameObject.layer = layer;

    }

    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            shieldBar.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
            BloodPrint();
            can.Display(life, shield);
        }
        else
        {
            shieldBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
        }

        CheckDoors();

        if (isLocalPlayer && life <= 0)
        {
            shield = 0;
            life = 0;
            alpha = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            quit.SetActive(true);
            escaped = true;


            text_pause.text = "GAME OVER";
        }

        if (!escaped && isLocalPlayer)
        {

            if (!cam.enabled)
                cam.enabled = true;
            if (!al.enabled)
                al.enabled = true;

            Moves();
            AnimPerso();

            if (Time.time > shieldCooldown)
            {
                ShieldRegeneration();
            }

            if (Time.time > nextFire)
            {
                if (Time.time > nextFire + 2 || isSurchauffe && Time.time > nextFire)
                {
                    if (nbTirs >= 0)
                    {
                        nbTirs--;
                    }

                    surchauffe_img.rectTransform.sizeDelta = new Vector2(nbTirs * (197f / nbTirsMax), 28); //20 tirs = 197 => 1 = 9.85
                }
                isSurchauffe = false;


                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (!isSurchauffe)
                    {
                        fireRate = 0.36f;
                        nextFire = Time.time + fireRate;
                        CmdFire();
                    }
                    //nbTirs = 0;
                    //suchauffe_img.rectTransform.sizeDelta = new Vector2(nbTirs * 9.85f, 28); //20 tirs = 197 => 1 = 9.85
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    if (nbTirs < nbTirsMax)
                    {
                        fireRate = 0.1f;
                        nbTirs++;
                        surchauffe_img.rectTransform.sizeDelta = new Vector2(nbTirs * (197f / nbTirsMax), 28); //20 tirs = 197 => 1 = 9.85
                        nextFire = Time.time + fireRate;
                        CmdBurst_Fire();
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
        }

    }
    #endregion


    #region otherMethods


    private void Moves()
    {
        //Deplacement
        transform.Translate(Input.GetAxis("Horizontal") * personnageSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * personnageSpeed * Time.deltaTime);

        //Jump
        if (Input.GetAxis("Jump") != 0f && isGrounded)
        {
            player.AddForce(0, jumpSpeed, 0);
            sound.PlayOneShot(jump);
            isGrounded = false;
        }

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            personnageSpeed = personnageSpeedRun;
            run = true;
        }
        else
        {
            personnageSpeed = personnageSpeedWalk;
            run = false;
        }

        //deplacement de la vue
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * sensibility, 0);


        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            Spine.transform.Rotate(0, 0, Input.GetAxisRaw("Mouse Y") * sensibility);
            Shoot.y = Input.GetAxisRaw("Mouse Y") * sensibility;
        }

        if (Spine.transform.eulerAngles.z > 330)
        {

            Spine.transform.eulerAngles = new Vector3(
                  Spine.transform.eulerAngles.x,
                   Spine.transform.eulerAngles.y,
                   330);
        }

        if (Spine.transform.eulerAngles.z < 200)
        {
            Spine.transform.eulerAngles = new Vector3(
                   Spine.transform.eulerAngles.x,
                   Spine.transform.eulerAngles.y,
                   200);
        }

        //debug des forces
        player.velocity = new Vector3(0, player.velocity.y, 0);

    }

    private void AnimPerso()
    {

        if (isSurchauffe)
        {
            animator.SetBool("isShooting", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", false);
            animator.SetBool("IsBienJoue", false);
            animator.SetBool("IsGogo", false);
            animator.SetBool("IsOk", false);
            //anim["surchauffe"].speed = 0.7f;
            //anim.Play("surchauffe");
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if (run)
            {
                animator.SetBool("isShooting", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("IsCoucou", false);
                animator.SetBool("IsBienJoue", false);
                animator.SetBool("IsGogo", false);
                animator.SetBool("IsOk", false);
                //anim["Walk"].speed = 4f;
            }
            else
            {
                animator.SetBool("isShooting", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("IsCoucou", false);
                animator.SetBool("IsBienJoue", false);
                animator.SetBool("IsGogo", false);
                animator.SetBool("IsOk", false);
                //anim["Walk"].speed = 2f;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", true);
            animator.SetBool("IsBienJoue", false);
            animator.SetBool("IsGogo", false);
            animator.SetBool("IsOk", false);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", false);
            animator.SetBool("IsBienJoue", true);
            animator.SetBool("IsGogo", false);
            animator.SetBool("IsOk", false);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", false);
            animator.SetBool("IsBienJoue", false);
            animator.SetBool("IsGogo", true);
            animator.SetBool("IsOk", false);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", false);
            animator.SetBool("IsBienJoue", false);
            animator.SetBool("IsGogo", false);
            animator.SetBool("IsOk", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("IsCoucou", false);
            animator.SetBool("IsBienJoue", false);
            animator.SetBool("IsGogo", false);
            animator.SetBool("IsOk", false);
            //anim.Play("Idle");
        }



    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy_atk" && isLocalPlayer)
        {
            Damage();
        }
        else
        {
            isGrounded = true;
        }
        player.velocity = new Vector3(0, 0, 0);

    }

    [Command]
    public void CmdFire()
    {

        /*GameObject balle;

        i++;

        Quaternion qua = new Quaternion(0, 0, 0, 0); //GetComponentInParent<Rigidbody>().transform.rotation.x

        balle = Instantiate(balleCasting.gameObject, BulletPos.transform.position, qua);

        balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection( Shoot ) * ejectSpeed;
        balle.GetComponent<Rigidbody>().isKinematic = false;*/

        GameObject balle = blabla.CmdFire(balleCasting, BulletPos.transform.position, weapon);

        NetworkServer.Spawn(balle);

        balle.name = "Bullet " + i;


        shoot.Play();
    }

    [Command]
    public void CmdBurst_Fire()
    {



        /*i++;
        balle = Instantiate(balleCasting.gameObject, BulletPos.transform.position, new Quaternion(BulletPos.transform.rotation.x,transform.rotation.y,BulletPos.transform.rotation.z,BulletPos.transform.rotation.w));
        balle.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * ejectSpeed;
        balle.GetComponent<Rigidbody>().isKinematic = false;*/

        GameObject balle = blabla.CmdBurst_Fire(balleCasting, BulletPos.transform.position, weapon);

        NetworkServer.Spawn(balle);

        balle.name = "Bullet " + i;

        shoot.Play();
    }

    #endregion


    [Command]
    public void CmdOpen_door(GameObject door, int nbMax)
    {
        if (transform.position.y <= nbMax)
        {
            door.transform.Translate(new Vector3(0, 0, 1));
        }

    }

    public bool FonctionNulleQuiRetrouneBool(GameObject door, int nbMax)
    {
        return door.transform.position.y > nbMax;
    }

    private void CheckDoors()
    {
        if (door1 == null || can.door1 == null)
        {
            door1 = GameObject.Find("door1");
            can.door1 = door1;
        }
        if (door2 == null || can.door2 == null)
        {
            door2 = GameObject.Find("door2");
            can.door2 = door2;
        }
        if (door3 == null || can.door3 == null)
        {
            door3 = GameObject.Find("door3");
            can.door3 = door3;
        }
        if (door4 == null || can.door4 == null)
        {
            door4 = GameObject.Find("door4");
            can.door4 = door4;
        }
        if (door5 == null || can.door5 == null)
        {
            door5 = GameObject.Find("door5");
            can.door5 = door5;
        }
        if (door6 == null || can.door6 == null)
        {
            door6 = GameObject.Find("door6");
            can.door6 = door6;
        }
        if (door7 == null || can.door7 == null)
        {
            door7 = GameObject.Find("door7");
            can.door7 = door7;
        }
        if (door8 == null || can.door8 == null)
        {
            door8 = GameObject.Find("door8");
            can.door7 = door7;
        }
        if (door9 == null || can.door9 == null)
        {
            door9 = GameObject.Find("door9");
            can.door9 = door9;
        }
        if (door10 == null || can.door10 == null)
        {
            door10 = GameObject.Find("door10");
            can.door10 = door10;
        }
        if (door11 == null || can.door11 == null)
        {
            door11 = GameObject.Find("door11");
            can.door11 = door11;
        }
        if (door12 == null || can.door12 == null)
        {
            door12 = GameObject.Find("door12");
            can.door12 = door12;
        }
        if (door13 == null || can.door13 == null)
        {
            door13 = GameObject.Find("door13");
            can.door13 = door13;
        }
        if (door14 == null || can.door14 == null)
        {
            door14 = GameObject.Find("door14");
            can.door14 = door14;
        }
        if (door15 == null || can.door15 == null)
        {
            door15 = GameObject.Find("door15");
            can.door15 = door15;
        }
    }

    private void Damage()
    {
        shieldCooldown = Time.time + 10;

        if (shield > 0)
        {
            shield -= damageTaken;
            if (shield < 0)
            {
                shield = 0;
            }
        }
        else
        {
            life -= damageTaken;

            alphamax += 0.025f;
            //colBlood.a += 0.025f;
            //blood.color = colBlood;

            if (life < 0)
            {
                life = 0;
            }
        }

        DisplayLife();
    }

    private void DisplayLife()
    {
        /*Placement des barres de vie/bouclier*/
        healthBar.rectTransform.sizeDelta = new Vector2(life * 2.25f, 30); //225 = 100 => 1 = 2.25
        shieldBar.rectTransform.sizeDelta = new Vector2(shield * 2.25f, 30);

        healthBar.rectTransform.transform.position = new Vector2(life * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y);
        shieldBar.rectTransform.transform.position = new Vector2(shield * 2.25f / 2 + 37, healthBar.rectTransform.transform.position.y - 55);
        /*Fin du placement*/

        //print(life + " life");
        //print(shield + " shield");
        //print(healthBar.rectTransform.transform.position);
        //print(shieldBar.rectTransform.transform.position);
    }

    private void ShieldRegeneration()
    {
        if (shield < 100)
        {
            shield++;
            DisplayLife();
        }
    }

    private void BloodPrint()
    {
        if (bloodUp)
        {
            if (alpha >= alphamax)
            {
                alpha = alphamax;
                bloodUp = false;
                colBlood.a = alpha;
                blood.color = colBlood;
            }
            else
            {
                alpha += 0.01f;
                if (alpha >= alphamax)
                    alpha = alphamax;
                colBlood.a = alpha;
                blood.color = colBlood;
            }
        }
        else
        {
            if (alpha <= 0f)
            {
                alpha = 0;
                bloodUp = true;
                colBlood.a = alpha;
                blood.color = colBlood;
            }
            else
            {
                alpha -= 0.01f;
                if (alpha < 0)
                    alpha = 0f;
                colBlood.a = alpha;
                blood.color = colBlood;
            }
        }
    }

}


//private Vector3 directionMove = Vector3.zero;

//directionMove.z = directionMove.z + Input.GetAxis("Vertical") * personnageSpeed * Time.deltaTime; //avant / arriere
//directionMove.x = directionMove.x + Input.GetAxis("Horizontal") * personnageSpeed * Time.deltaTime; //gauche / droite


//deplacement
//directionMove = transform.TransformDirection(directionMove.x * personnageSpeed, directionMove.y, directionMove.z * personnageSpeed);
//directionMove *= Time.deltaTime;

/*if (!player.isGrounded)
    directionMove.y -= gravity * Time.deltaTime;*/

//player.Move(directionMove);