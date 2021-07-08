using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    TextMeshPro text;
    Color alpha;
    public int dmg;
    public bool isCritical;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = dmg.ToString();
        if (isCritical)
        {
            text.color = new Color(1, 0, 0, 1);
            text.fontSize = 8;
            text.text = dmg.ToString() + "!";
        }
        Invoke("DestroyObj", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime), 0);
        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime + alphaSpeed);
        //text.color = alpha;
    }
    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
