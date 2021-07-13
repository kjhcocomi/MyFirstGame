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
    public GameObject dmgText;

    public Transform hudPos;

    public SpriteRenderer spr;
    public Vector2 size;
    public LayerMask whatIsLayer;

    public Sprite[] sprites;

    public Rigidbody2D rigid;

    public GameObject bulletObj;
    public GameObject CoinObjG;
    public GameObject CoinObjB;
    public GameObject CoinObjR;
    public GameObject MedObj;
    public GameObject chestObj;

    public Slider slider;

    public bool findplayer;
    public bool isLongDistance;

    Vector3 latePos;

    float h;
    float v;
    float positionCheckCount;

    void Start()
    {

    }


    void Update()
    {
        ManageHPbar();
        FindPlayer();
        CheckLatePosition();
        Move();
        Reload();
        checkDie();
    }
    void CheckLatePosition()
    {
        positionCheckCount += Time.deltaTime;
        if (positionCheckCount >= 2)
        {
            latePos = gameObject.transform.position;
            positionCheckCount = 0;
        }
    }
    void Move()
    {
        currMoveDelay+=Time.deltaTime;
        if (findplayer)
        {
            if (!isLongDistance)
            {
                rigid.velocity = (player.transform.position - transform.position).normalized * speed;
            }
            else
            {
                rigid.velocity = new Vector3(0, 0, 0);
                Fire();
            }
        }
        else if (currMoveDelay > maxMoveDelay)
        {
            /*
            h = Random.Range(-1, 2);
            v = Random.Range(-1, 2);
            rigid.velocity = new Vector2(h, v).normalized * speed;
            currMoveDelay = 0;
            */
        }
    }
    void FindPlayer()
    {
        bool isfind = false;
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, size, 0, whatIsLayer);
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].name == "Player")
            {
                findplayer = true;
                isfind = true;
            }
        }
        if (!isfind) findplayer = false;
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
    void checkDie()
    {
        if (currhealth <= 0)
        {
            Destroy(gameObject);
            CreateItem();
        }
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
            int randdmg = Random.Range(0, (int)player.attack + 3);
            int rancridmg = Random.Range((int)player.attack + 2, (int)player.attack + 5);
            if (player.critical_chance >= randval)
            {
                Debug.Log("Critical Hit!");
                OnHit((weapon.dmg[weapon.type] * player.attack) * player.critical_damage+rancridmg, collision, true);
            }
            else
            {
                OnHit(weapon.dmg[weapon.type] * player.attack + randdmg, collision, false);
            }
        }
        else if (collision.tag == "Border")
        {
            rigid.velocity = Vector3.zero;
            transform.position = latePos;
        }
    }

    void OnHit(float dmg, Collider2D collision, bool isCritical)
    {
        setDmgText((int)dmg, isCritical);
        HitAnimation(collision);
        currhealth -= (int)dmg;
    }
    void CreateItem()
    {
        int randval = Random.Range(1, 101);
        if (randval > 0 && randval <= 25)
        {
            Instantiate(CoinObjG, transform.position, transform.rotation);
        }
        else if (randval > 25 && randval <= 50)
        {
            Instantiate(CoinObjB, transform.position, transform.rotation);
        }
        else if (randval > 50 && randval <= 70)
        {
            Instantiate(CoinObjR, transform.position, transform.rotation);
        }
        else if (randval > 70 && randval <= 85)
        {
            Instantiate(MedObj, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(chestObj, transform.position, transform.rotation);
        }
    }
    void HitAnimation(Collider2D collision)
    {
        spr.color = new Color(1, 1, 1, 0.4f);
        Invoke("colorback", 0.1f);
        transform.position -= (collision.transform.position - transform.position)*weapon.knockback;
    }
    void setDmgText(int dmg, bool isCritical)
    {
        dmgText.GetComponent<Damage>().dmg = (int)dmg;
        dmgText.GetComponent<Damage>().isCritical = isCritical;
        GameObject dmgtext = Instantiate(dmgText, hudPos.position, transform.rotation);
    }
    void colorback()
    {
        spr.color = new Color(1, 1, 1, 1);
    }
}
