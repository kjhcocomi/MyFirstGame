using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject bulletObj;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyBullet", 1f);
    }
    void destroyBullet()
    {
        GameObject bullet1 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet2 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet3 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet4 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet5 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet6 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet7 = Instantiate(bulletObj, transform.position, transform.rotation);
        GameObject bullet8 = Instantiate(bulletObj, transform.position, transform.rotation);
        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = bullet4.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid5 = bullet5.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid6 = bullet6.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid7 = bullet7.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid8 = bullet8.GetComponent<Rigidbody2D>();
        Vector3 dirVec1 = new Vector3(0, 1, 0);
        Vector3 dirVec2 = new Vector3(1, 1, 0);
        Vector3 dirVec3 = new Vector3(1, 0, 0);
        Vector3 dirVec4 = new Vector3(1, -1, 0);
        Vector3 dirVec5 = new Vector3(0, -1, 0);
        Vector3 dirVec6 = new Vector3(-1, -1, 0);
        Vector3 dirVec7 = new Vector3(-1, 0, 0);
        Vector3 dirVec8 = new Vector3(-1, 1, 0);
        rigid1.AddForce(dirVec1.normalized * 5f, ForceMode2D.Impulse);
        rigid2.AddForce(dirVec2.normalized * 5f, ForceMode2D.Impulse);
        rigid3.AddForce(dirVec3.normalized * 5f, ForceMode2D.Impulse);
        rigid4.AddForce(dirVec4.normalized * 5f, ForceMode2D.Impulse);
        rigid5.AddForce(dirVec5.normalized * 5f, ForceMode2D.Impulse);
        rigid6.AddForce(dirVec6.normalized * 5f, ForceMode2D.Impulse);
        rigid7.AddForce(dirVec7.normalized * 5f, ForceMode2D.Impulse);
        rigid8.AddForce(dirVec8.normalized * 5f, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
