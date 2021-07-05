using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float curr_health;
    public float max_health;
    public float speed;
    public float attack;
    public float critical_chance;
    public float critical_damage;
    float h;
    float v;

    int rayh;
    int rayv;
    int firstdir;

    public Rigidbody2D rigid;

    public Animator anim;
    GameObject ScanObject;

    void Start()
    {
        
    }

    void Update()
    {
        InputMove();
        InputScan();
    }
    void InputMove()
    {
        //h = gm.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        //v = gm.isAction ? 0 : Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v != 0) firstdir = 0;
        else if (h != 0 && v == 0) firstdir = 1;

        if (h != 0 && v != 0)
        {
            if (firstdir == 0) v = 0;
            else h = 0;
        }
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
    void SetAnimation()
    {
        if (anim.GetInteger("HorizonAxisRaw") != h)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("HorizonAxisRaw", (int)h);
        }
        else if (anim.GetInteger("VerticalAxisRaw") != v)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("VerticalAxisRaw", (int)v);
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
}
