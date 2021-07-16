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


    float findCount;
    float curBossAttackCount = 3;

    public Player player;
    public Weapon weapon;
    public GameManager gm;
    public GameObject dmgText;

    Animator anim;

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
    public GameObject bulletBossObj;

    public UIManager um;

    public Slider slider;

    public bool findplayer;
    public bool isLongDistance;

    AudioSource ads;

    public AudioClip banana;
    public AudioClip damaged;
    public AudioClip die;

    Vector3 latePos;

    float h;
    float v;
    float positionCheckCount;
    float maxFindCount = 0.5f;
    float maxBossShotDelay = 0.9f;
    float curBossShotDelay;
    float maxBossMoveDelay = 3f;
    float curBossMoveDelay;

    int attackWhat;

    void Start()
    {
        ads = GetComponent<AudioSource>();
        anim = GetComponent<Animator>(); 
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
        curBossMoveDelay += Time.deltaTime;
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
                if (enemyName == "S")
                {
                    Fire();
                }
                else if (enemyName == "B")
                {
                    BossFire();
                }
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
        findCount += Time.deltaTime;
        if (findCount >= maxFindCount)
        {
            maxFindCount = Random.Range(0.3f, 0.7f);
            FindPlayer2();
            findCount = 0;
        }
    }
    void FindPlayer2()
    {
        bool isfind = false;
        Collider2D hit = Physics2D.OverlapBox(transform.position, size, 0, whatIsLayer);

        if (hit != null)
        {
            if (hit.name == "Player")
            {
                findplayer = true;
                isfind = true;
            }
        }
        if (!isfind)
        {
            findplayer = false;
        }
    }
    void Fire()
    {
        if (currShotDelay < maxShotDelay)
        {
            return;
        }
        if (enemyName == "S")
        {
            playSound("banana");
            anim.SetBool("isFind", true);
            Invoke("returnAnimation", 0.2f);
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
        }      
        currShotDelay = 0;
    }
    void BossFire()
    {
        curBossAttackCount += Time.deltaTime;
        if (curBossAttackCount >= 3)
        {
            attackWhat = Random.Range(0, 3);
            curBossAttackCount = 0;
        }
        if (attackWhat == 0)
        {
            if (currShotDelay < maxShotDelay)
            {
                return;
            }
            playSound("banana");
            anim.SetBool("isFind", true);
            Invoke("returnAnimation", 0.1f);
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * bulletSpeed, ForceMode2D.Impulse);
            currShotDelay = 0;
        }
        else if (attackWhat == 1)
        {
            if (curBossShotDelay < maxBossShotDelay)
            {
                return;
            }
            playSound("banana");
            anim.SetBool("isFind", true);
            Invoke("returnAnimation", 0.2f);
            GameObject bulletb = Instantiate(bulletBossObj, transform.position, transform.rotation);
            Rigidbody2D rigidb = bulletb.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigidb.AddForce(dirVec.normalized * bulletSpeed * 0.6f, ForceMode2D.Impulse);
            curBossShotDelay = 0;
        }
        else if (attackWhat == 2)
        {
            if (curBossMoveDelay < maxBossMoveDelay)
            {
                return;
            }
            attackWhat = Random.Range(0, 2);
            transform.position = player.transform.position + Vector3.up*2f;
            curBossMoveDelay = 0;
        }
    }
    void returnAnimation()
    {
        anim.SetBool("isFind", false);
    }
   void Reload()
    {
        currShotDelay += Time.deltaTime;
        curBossShotDelay += Time.deltaTime;
    }
    void checkDie()
    {
        if (currhealth <= 0)
        {
            if (enemyName == "B")
            {
                um.ActionClearUI();
            }
            else
            {
                player.coin += 1;
                Destroy(gameObject);
                CreateItem();
            }
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
            playSound("damaged");
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
        /*
        else if (collision.tag == "Border")
        {
            rigid.velocity = Vector3.zero;
            transform.position = latePos;
        }
        */
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
        if (enemyName == "S")
        {
            transform.position -= (collision.transform.position - transform.position) * weapon.knockback;
        }
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
    void playSound(string str)
    {
        if (str == "banana")
        {
            ads.clip = banana;
        }
        else if (str == "damaged")
        {
            ads.clip = damaged;
        }
        else if (str == "die")
        {

        }
        ads.Play();
    }
}
