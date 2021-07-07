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

    int mapCount;
    void Start()
    {
        Time.timeScale = 0;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
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
}
