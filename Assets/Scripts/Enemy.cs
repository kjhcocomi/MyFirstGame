using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public string enemyName;
    public float maxhealth;
    public float currhealth;
    public float speed;
    public float currMoveDelay;
    public float maxMoveDelay;
    public float maxShotDelay;
    public float currShotDelay;
    public float bulletSpeed;
    public float attack;

    public Player player;
    public Weapon weapon;
    public GameManager gm;

    public SpriteRenderer spr;
    public Vector2 size;
    public LayerMask whatIsLayer;

    public Sprite[] sprites;

    public Rigidbody2D rigid;

    public GameObject bulletObj;

    public Slider slider;

    public bool findplayer;
    public bool isLongDistance;

    float h;
    float v;

    void Start()
    {
        
    }


    void Update()
    {
        ManageHPbar();
        FindPlayer();
        Move();
        Reload();
    }
    void Move()
    {
        currMoveDelay+=Time.deltaTime;
        if (findplayer)
        {
            if (!isLongDistance)
            {
                rigid.velocity = (player.transform.position - transform.position) * speed * 0.3f;
            }
            else
            {
                rigid.velocity = new Vector3(0, 0, 0);
                Fire();
            }
        }
        else if (currMoveDelay > maxMoveDelay)
        {
            h = Random.Range(-1, 2);
            v = Random.Range(-1, 2);
            rigid.velocity = new Vector2(h, v).normalized * speed;
            currMoveDelay = 0;
        }
    }
    void FindPlayer()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, size, 0, whatIsLayer);
        if (hit.name == "Player")
        {
            findplayer = true;
        }

    }
    void Fire()
    {
        if (currShotDelay < maxShotDelay) return;
        if (enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
        currShotDelay = 0;
    }
   void Reload()
    {
        currShotDelay += Time.deltaTime;
    }
    void ManageHPbar()
    {
        slider.maxValue = maxhealth;
        slider.minValue = 0;
        slider.value = currhealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (!player.ispenetrate) Destroy(collision.gameObject);
            int randval=Random.Range(1, 101);
            if (player.critical_chance >= randval)
            {
                Debug.Log("Critical Hit!");
                OnHit(weapon.dmg * player.attack * player.critical_damage,collision);
            }
            else
            {
                OnHit(weapon.dmg * player.attack,collision);
            }
        }
        else if (collision.tag == "Player")
        {

        }
    }

    void OnHit(float dmg, Collider2D collision)
    {
        HitAnimation(collision);
        currhealth -= dmg;
        Debug.Log(currhealth);
        if (currhealth <= 0) 
        {
            Destroy(gameObject);
        }
    }
    void HitAnimation(Collider2D collision)
    {
        spr.color = new Color(1, 1, 1, 0.4f);
        Invoke("colorback", 0.1f);
        transform.position -= (collision.transform.position - transform.position)*weapon.knockback;
    }
    void colorback()
    {
        spr.color = new Color(1, 1, 1, 1);
    }
}
