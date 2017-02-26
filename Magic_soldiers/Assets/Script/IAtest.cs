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

    public int life;

    private int rotX = 0;

    public float speed;
    public float modif1;
    public float modif2;

    Animator animator;
    bool isWalking = true;
    bool isAttacking = false;

    private float xPlayer;
    private float zPlayer;
    private int DetectRadius;

    private UnityEngine.UI.Text lifeBar;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
        pattern = true;
        posX0 = transform.position.x;
        posZ0 = transform.position.z;
        posZ = 0;
        posX = -10;
        pos = 0;

        lifeBar = transform.FindChild("Life_Bar").gameObject.GetComponent<UnityEngine.UI.Text>();

        LifeDisplay();

        xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;
        DetectRadius = 25;

    }

    // Update is called once per frame
    void Update() {


        if (life > 0)
        {
            transform.FindChild("Life_Bar").LookAt(GameObject.Find("Perso").transform);
            DetectPlayer();

            if (pattern)
            {
                Pattern();
            }
            else
            {
                Attack();
            }

            if ((isWalking) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                animator.SetTrigger("Walk");
            }

            transform.position = new Vector3(posX0 + posX, 0, posZ0 + posZ);
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                animator.SetTrigger("Death");
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
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

        transform.LookAt(GameObject.Find("Perso").transform);

        xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;


        if ((!isAttacking) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {

            if (xPlayer - 1.6F <= posX0 + posX)
            {
                posX -= Time.deltaTime * speed * 2;
            }
            if (xPlayer + 1.6F >= posX0 + posX)
            {
                posX += Time.deltaTime * speed * 2;
            }
            if (zPlayer - 1.6F <= posZ0 + posZ)
            {
                posZ -= Time.deltaTime * speed * 2;
            }
            if (zPlayer + 1.6F >= posZ0 + posZ)
            {
                posZ += Time.deltaTime * speed * 2;
            }
        }

        if((xPlayer - (posX0 + posX)) * (xPlayer - (posX0 + posX)) + (zPlayer - (posZ0 + posZ)) * (zPlayer - (posZ0 + posZ)) <= 10)
        {
            isAttacking = true;
            isWalking = false;
            animator.SetTrigger("Attack");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isAttacking = false;
            isWalking = true;
        }
        
    }

    private void DetectPlayer()
    {
        xPlayer = GameObject.Find("Perso").transform.position.x;
        zPlayer = GameObject.Find("Perso").transform.position.z;
        if (pattern == true)
        {
            pattern = !((xPlayer - (posX0 + posX)) * (xPlayer - (posX0 + posX)) + (zPlayer - (posZ0 + posZ)) * (zPlayer - (posZ0 + posZ)) <= DetectRadius * DetectRadius);
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
        string s = "";
        string name = col.gameObject.name;
        int i = 0;

        while ((i < name.Length) && (s != "Bullet"))
        {
            s += name[i];
            i++;
        }

        if(s == "Bullet")
        {
            life -= 1;
            LifeDisplay();
            Destroy(col.gameObject);
        }
    }
}
