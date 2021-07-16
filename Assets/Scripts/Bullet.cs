using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!(gameObject.tag == "EnemyBullet"))
        {
            Invoke("destroyBullet", 0.48f);
        }
        else
        {
            Invoke("destroyEnemyBullet", 2f);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border"|| collision.gameObject.tag == "Rock")
        {
            Destroy(gameObject);
        }
    }
    void destroyBullet()
    {
        Destroy(gameObject);
    }
    void destroyEnemyBullet()
    {
        Destroy(gameObject);
    }
}
