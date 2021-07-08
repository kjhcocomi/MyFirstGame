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

    public bool isSkill1;
    public bool isInvincible;

    int rayh;
    int rayv;
    int firstdir;
    int beforeh;
    public int coin;
    public int med;

    float RecoveryCount;

    public GameObject grid;
    public GameObject skill2Effect;
    public GameObject skill1Effect;
    public Shield shield;

    public Enemy enemy;

    public SpriteRenderer spr;

    public Rigidbody2D rigid;

    public Animator anim;

    public GameManager gm;
    public UIManager um;
    GameObject ScanObject;

    void Start()
    {
        isSkill1 = false;
        isInvincible = false;
    }

    void Update()
    {
        InputMove();
        InputScan();
        ShieldRecovery();
        ScanSkillInput();
        ScanDrinkInput();
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
            gm.Action(ScanObject);
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
    void ScanSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (um.currskillcooltime1 >= um.maxskillcooltime1)
            {
                skill1Effect.SetActive(true);
                isSkill1 = true;
                Invoke("skill1Back", 10f);
                um.currskillcooltime1 = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (um.currskillcooltime2 >= um.maxskillcooltime2)
            {
                skill2Effect.SetActive(true);
                isInvincible = true;
                Invoke("skill2Back", 5f);
                um.currskillcooltime2 = 0;
            }

        }

    }
    void ScanDrinkInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (curr_health < max_health && med > 0)
            {
                curr_health += 3;
                med--;
                if (curr_health > max_health)
                {
                    curr_health = max_health;
                }
            }
        }
    }
    void skill1Back()
    {
        skill1Effect.SetActive(false);
        isSkill1 = false;
    }
    void skill2Back()
    {
        skill2Effect.SetActive(false);
        isInvincible = false;
        spr.color = new Color(1, 1, 1, 1);
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
        CheckShield();
    }
    void Move()
    {
        rigid.velocity = new Vector2(h, v) * speed;
    }
    void ShootRay()
    {
        Vector2 RayVec = new Vector2(rayh, rayv)*0.5f;
        Debug.DrawRay(rigid.position, RayVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, RayVec, 0.5f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
            Debug.Log("gd");
        }
        else
        {
            ScanObject = null;
        }
    }
    void CheckShield()
    {
        if (curr_shield < 1)
        {
            shield.spr.color = new Color(1, 1, 1, 0);
        }
        else
        {
            shield.spr.color = new Color(1, 1, 1, 0.6f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {     
            if (!isInvincible)
            {
                Destroy(collision.gameObject);
                OnHit(collision, 1f);
            }
        }
        else if (collision.tag == "portal")
        {
            gm.Action(collision);           
        }
        else if (collision.tag == "GoldCoin")
        {
            coin += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "BlueCoin")
        {
            coin += 2;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "RedCoin")
        {
            coin += 3;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Med")
        {
            med+=1;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!isInvincible)
            {
                OnHit(collision, 1f);
            }
        }
    }
    void OnHit(Collider2D collision, float dmg)
    {
        if (curr_shield > 0)
        {
            curr_shield -= dmg;
            shield.spr.color = new Color(1, 1, 1, 0.1f);
            if (curr_shield < 0) curr_shield = 0;
            isInvincible = true;
            Invoke("colorBack", 0.3f);
        }
        else
        {
            curr_health -= dmg;
            spr.color = new Color(1, 1, 1, 0.6f);
            transform.position -= (collision.transform.position - transform.position) * 0.2f;
            isInvincible = true;
            Invoke("colorBack", InvincibleTime);
        }
        if (curr_health <= 0)
        {
            gm.PlayerDie();
        }
    }
    void colorBack()
    {
        if(!skill2Effect.activeSelf) isInvincible = false;
        spr.color = new Color(1, 1, 1, 1);
    }
}
