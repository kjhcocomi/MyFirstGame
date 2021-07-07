using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    Vector3 MousePosition;
    Vector3 target;
    Camera Camera;

    public Player player;

    public float currShootDelay;
    public float maxShootDelay;
    public float speed;
    public float dmg;
    public float knockback;

    public int type;

    public GameObject[] bulletObj;

    public SpriteRenderer spr;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        ChangeWeaponPosition();
        ChangeWeaponRotation();
        //PrintMousePosition();
        MousePosition = Input.mousePosition;
        Fire();
        Reload();
    }
    void ChangeWeaponPosition()
    {
        transform.position = player.transform.position-new Vector3(0,0.2f,0);
    }
    void ChangeWeaponRotation()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        target = Camera.main.ScreenToWorldPoint(mPosition);
        if (target.x < transform.position.x)
        {
            spr.flipY = true;
        }
        else spr.flipY = false;

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }
    void PrintMousePosition()
    {
        MousePosition = Input.mousePosition;
        Debug.Log(MousePosition);
    }
    void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (currShootDelay < maxShootDelay/player.attack_speed) return;

        if (type == 0)
        {
            if (!player.isSkill1)
            {
                GameObject bullet = Instantiate(bulletObj[0], transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce((target - player.transform.position).normalized * speed, ForceMode2D.Impulse);
            }
            else
            {
                GameObject bulletA = Instantiate(bulletObj[0], transform.position + new Vector3(0, 1, 0) * 0.1f, transform.rotation);
                GameObject bulletB = Instantiate(bulletObj[0], transform.position - new Vector3(0, 1, 0) * 0.1f, transform.rotation);
                Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
                rigidA.AddForce((target - player.transform.position).normalized * speed, ForceMode2D.Impulse);
                rigidB.AddForce((target - player.transform.position).normalized * speed, ForceMode2D.Impulse);
            }

        }
        currShootDelay = 0;
    }
    void Reload()
    {
        currShootDelay += Time.deltaTime;
    }

}

