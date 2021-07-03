using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    float h;
    float v;

    int firstdir;

    public Rigidbody2D rigid;

    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        InputMove();
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
        SetAnimation();
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
    }
    void Move()
    {
        rigid.velocity = new Vector2(h, v) * speed;
    }
}
