using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float curr_health;
    public float max_health;
    public float curr_shield;
    public float max_shield;
    public float attack_speed;

    public float speed;
    public float attack;
    public float critical_chance;
    public float critical_damage;
    public float InvincibleTime;
    public bool ispenetrate;
    float h;
    float v;

    int rayh;
    int rayv;
    int firstdir;
    int beforeh;

    float RecoveryCount;

    public Enemy enemy;

    public SpriteRenderer spr;

    public Rigidbody2D rigid;

    public Animator anim;

    public GameManager gm;
    GameObject ScanObject;

    void Start()
    {
        
    }

    void Update()
    {
        InputMove();
        InputScan();
        ShieldRecovery();
    }
    void InputMove()
    {
        //h = gm.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        //v = gm.isAction ? 0 : Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        /*
        if (h == 0 && v != 0) firstdir = 0;
        else if (h != 0 && v == 0) firstdir = 1;

        if (h != 0 && v != 0)
        {
            if (firstdir == 0) v = 0;
            else h = 0;
        }
        */
        GetPlayerRay();
        SetAnimation();
    }
    void GetPlayerRay()
    {
        if (h != 0)
        {
            rayh = (int)h;
            rayv = 0;
        }
        else if (v != 0)
        {
            rayh = 0;
            rayv = (int)v;
        }
    }
    void InputScan()
    {
        /*
        if (Input.GetButtonDown("Jump") && ScanObject != null)
        {
            //gm.Action(ScanObject);
        }
        */
    }
    void ShieldRecovery()
    {
        RecoveryCount += Time.deltaTime;
        if (RecoveryCount >= 5)
        {
            if (curr_shield < max_shield)
            {
                curr_shield++;
            }
            RecoveryCount = 0;
        }
    }
    void SetAnimation()
    {
        if (anim.GetInteger("HorizonAxisRaw") != h)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("HorizonAxisRaw", (int)h);
            if (h!= 0)
            {
                beforeh = (int)h;
            }
        }
        else if (anim.GetInteger("VerticalAxisRaw") != v)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("VerticalAxisRaw", (int)v);
            anim.SetInteger("BeforeH", beforeh);
        }
        else anim.SetBool("IsChange", false);
    }
    void FixedUpdate()
    {
        Move();
        ShootRay();
    }
    void Move()
    {
        rigid.velocity = new Vector2(h, v) * speed;
    }
    void ShootRay()
    {
        Vector2 RayVec = new Vector2(rayh, rayv);
        Debug.DrawRay(rigid.position, RayVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, RayVec, 1f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if(gameObject.tag!= "Invincible")
            {
                OnHit(collision, 1f);
            }
        }
        else if (collision.tag == "EnemyBullet")
        {     
            if (gameObject.tag != "Invincible")
            {
                Destroy(collision.gameObject);
                OnHit(collision, 1f);
            }
        }
    }
    void OnHit(Collider2D collision, float dmg)
    {
        if (curr_shield > 0)
        {
            curr_shield -= dmg;
            if (curr_shield < 0) curr_shield = 0;
        }
        else curr_health -= dmg;
        spr.color = new Color(1, 1, 1, 0.4f);
        if (curr_health <= 0)
        {
            gm.Playerdie();
        }
        gameObject.tag = "Invincible";
        transform.position -= (collision.transform.position - transform.position)*0.2f;
        Invoke("colorBack", InvincibleTime);
    }
    void colorBack()
    {
        gameObject.tag = "Player";
        spr.color = new Color(1, 1, 1, 1);
    }
}
