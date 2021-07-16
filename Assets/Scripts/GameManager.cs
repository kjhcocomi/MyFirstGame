using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public UIManager um;
    public Texture2D cursorArrow;
    public Player player;
    public GameObject[] maps;

    public string[] chestString = { "���ݷ�����", "���ݼӵ�����", "�ִ�HP����", "�ִ뽯������" 
                                    ,"ũ��Ƽ�� Ȯ�� ����", "ũ��Ƽ�� ������ ����", "�̵��ӵ� ����"
                                    ,"�ǰݽ� �����ð� ����", "z��ų ��Ÿ�� ����", "x��ų ��Ÿ�� ����"
                                    , "c��ų ��Ÿ�� ����", "���� ȸ���� ����", "�����Ѿ�"};
    public int[] weaponPrice;
    int mapCount;

    public float score;
    public Vector2 size;
    public LayerMask whatIsLayer;

    void Start()
    {
        Time.timeScale = 0;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
    private void Update()
    {
        score += Time.deltaTime;
    }
    // Update is called once per frame
    public void PlayerDie()
    {
        player.spr.flipY = true;
        player.spr.color = new Color(1, 1, 1, 0.4f);
        Time.timeScale = 0;
        um.playerDead();
    }
    public void Action(Collider2D collision)
    {
        if (collision.tag == "portal")
        {
            goNextMap();
        }
    }
    public void goNextMap()
    {
        player.transform.position = Vector3.zero;
        maps[mapCount++].SetActive(false);
        maps[mapCount].SetActive(true);
    }
    public void PushChestButton(int num)
    {
        if (num == 0)
        {
            player.attack += 0.5f;
        }
        else if (num == 1)
        {
            player.attack_speed += 0.5f;
        }
        else if (num == 2)
        {
            player.max_health += 3;
        }
        else if (num == 3)
        {
            player.max_shield += 1;
        }
        else if (num == 4)
        {
            player.critical_chance += 10;
        }
        else if (num == 5)
        {
            player.critical_damage +=0.3f;
        }
        else if (num == 6)
        {
            player.speed += 0.2f;
            player.currSpeed += 0.2f;
        }
        else if (num == 7)
        {
            player.InvincibleTime += 0.3f;
        }
        else if (num == 8)
        {
            um.maxskillcooltime3 *= 0.8f;
        }
        else if (num == 9)
        {
            um.maxskillcooltime2 *= 0.8f;
        }
        else if (num == 10)
        {
            um.maxskillcooltime1 *= 0.8f;
        }
        else if (num == 11)
        {
            player.recoveryAmount += 1;
        }
        else if (num == 12)
        {
            player.ispenetrate = true;
        }
    }
}
