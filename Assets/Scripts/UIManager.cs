using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gm;
    public Slider PlayerHPbar;
    public Slider PlayerShieldbar;
    public Text HPText;
    public Text ShieldText;
    public GameObject backGround;
    public GameObject CharacterUI;
    public GameObject menu;
    public GameObject DeadUI;
    public GameObject clearUI;

    public Text attackText;
    public Text attackSpeedText;
    public Text criticalChanceText;
    public Text criticalDamageText;
    public Text InvincibleTimeText;
    public Text playerSpeedText;
    public Text CoinText;
    public Text medText;
    public Text keyText;
    public Text chest1;
    public Text chest2;
    public Text chest3;
    public Text scoreText;

    public GameObject Skill1UI;
    public GameObject Skill2UI;
    public GameObject Skill3UI;
    public GameObject ItemUI;
    public GameObject ChestUI;
    public Image skill1;
    public Image skill2;
    public Image skill3;

    public Player player;

    public int buttonNum1;
    public int buttonNum2;
    public int buttonNum3;

    public float currskillcooltime1;
    public float maxskillcooltime1;
    public float currskillcooltime2;
    public float maxskillcooltime2;
    public float currskillcooltime3;
    public float maxskillcooltime3;

    int maxnum;
    void Start()
    {
        currskillcooltime1=60;
        maxskillcooltime1=60;
        currskillcooltime2 = 60;
        maxskillcooltime2 = 60;
        currskillcooltime3 = 30;
        maxskillcooltime3 = 30;
        PlayerHPbar.maxValue = player.max_health;
        PlayerHPbar.minValue = 0;
        PlayerShieldbar.maxValue = player.max_shield;
        PlayerShieldbar.minValue = 0;
    }

    void Update()
    {
        ManagePlayerHP();
        ManagePlyaerShield();
        ManageStats();
        CheckPressMenu();
        CheckSkillCoolTime();
        checkItem();
    }
    void ManagePlayerHP()
    {
        PlayerHPbar.maxValue = player.max_health;
        PlayerHPbar.value = player.curr_health;
        HPText.text = player.curr_health + " / " + player.max_health;
    }
    void ManagePlyaerShield()
    {
        PlayerShieldbar.maxValue = player.max_shield;
        PlayerShieldbar.value = player.curr_shield;
        ShieldText.text = player.curr_shield + " / " + player.max_shield;
    }
    void CheckPressMenu()
    {
        if (Input.GetButtonDown("Cancel") && menu.activeSelf)
        {
            player.rigid.velocity = Vector2.zero;
            Time.timeScale = 1;
            menu.SetActive(false);
        }
        else if (Input.GetButtonDown("Cancel")&&!backGround.activeSelf&&!DeadUI.activeSelf)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }
    void CheckSkillCoolTime()
    {
        if (currskillcooltime1 < maxskillcooltime1)
        {
            currskillcooltime1 += Time.deltaTime;
        }
        if (currskillcooltime2 < maxskillcooltime2)
        {
            currskillcooltime2 += Time.deltaTime;
        }
        if (currskillcooltime3 < maxskillcooltime3)
        {
            currskillcooltime3 += Time.deltaTime;
        }
        skill1.fillAmount = currskillcooltime1 / maxskillcooltime1;
        skill2.fillAmount = currskillcooltime2 / maxskillcooltime2;
        skill3.fillAmount = currskillcooltime3 / maxskillcooltime3;
    }
    void checkItem()
    {
        if (player.currWeaponLevel == 7)
        {
            CoinText.text = player.coin.ToString();
        }
        else
        {
            CoinText.text = player.coin.ToString() + "/" + gm.weaponPrice[player.currWeaponLevel].ToString();
        }
        medText.text = player.med.ToString();
        keyText.text = player.key.ToString();
    }
    void ManageStats()
    {
        attackText.text = "??????: " + player.attack;
        attackSpeedText.text = "????????: " + player.attack_speed;
        criticalChanceText.text = "????????: " + player.critical_chance + "%";
        criticalDamageText.text = "????????: " + player.critical_damage * 100 + "%";
        InvincibleTimeText.text = "????????: " + player.InvincibleTime;
        playerSpeedText.text = "????????: " + player.speed;
    }

    public void pressFirstStart()
    {
        player.rigid.velocity = Vector2.zero;
        Time.timeScale = 1;
        backGround.SetActive(false);
        CharacterUI.SetActive(true);
        Skill1UI.SetActive(true);
        Skill2UI.SetActive(true);
        Skill3UI.SetActive(true);
        ItemUI.SetActive(true);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void keepGoing()
    {
        player.rigid.velocity = Vector2.zero;
        Time.timeScale = 1;
        menu.SetActive(false);
    }
    public void reStart()
    {
        if (DeadUI.activeSelf) DeadUI.SetActive(false);
        if (clearUI.activeSelf) clearUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void playerDead()
    {
        DeadUI.SetActive(true);
    }
    public void ActionChestEvent()
    {
        if (player.ispenetrate)
        {
            maxnum = gm.chestString.Length-1;
        }
        else
        {
            maxnum = gm.chestString.Length;
        }

        buttonNum1 = Random.Range(0, maxnum);
        buttonNum2 = Random.Range(0, maxnum);
        while (buttonNum2 == buttonNum1)
        {
            buttonNum2 = Random.Range(0, maxnum);
        }
        buttonNum3 = Random.Range(0, maxnum);
        while ((buttonNum3 == buttonNum2)||(buttonNum3 == buttonNum1))
        {
            buttonNum3 = Random.Range(0, maxnum);
        }
        chest1.text = gm.chestString[buttonNum1];
        chest2.text = gm.chestString[buttonNum2];
        chest3.text = gm.chestString[buttonNum3];
        Time.timeScale = 0;
        ChestUI.SetActive(true);
    }
    public void PushChestButton1()
    {
        ChestUI.SetActive(false);
        Time.timeScale = 1;
        gm.PushChestButton(buttonNum1);
    }
    public void PushChestButton2()
    {
        ChestUI.SetActive(false);
        Time.timeScale = 1;
        gm.PushChestButton(buttonNum2);
    }
    public void PushChestButton3()
    {
        ChestUI.SetActive(false);
        Time.timeScale = 1;
        gm.PushChestButton(buttonNum3);
    }
    public void ActionClearUI()
    {
        Time.timeScale = 0;
        scoreText.text = "?????? ????: " + ((int)gm.score).ToString() + " ??";
        clearUI.SetActive(true);
    }
}
