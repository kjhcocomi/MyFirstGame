using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider PlayerHPbar;
    public Slider PlayerShieldbar;
    public Text HPText;
    public Text ShieldText;
    public GameObject backGround;
    public GameObject CharacterUI;
    public GameObject menu;
    public GameObject DeadUI;

    public Text attackText;
    public Text attackSpeedText;
    public Text criticalChanceText;
    public Text criticalDamageText;
    public Text InvincibleTimeText;

    public GameObject Skill1UI;
    public GameObject Skill2UI;
    public Image skill1;
    public Image skill2;

    public Player player;

    public float currskillcooltime1;
    public float maxskillcooltime1;
    public float currskillcooltime2;
    public float maxskillcooltime2;
    void Start()
    {
        currskillcooltime1=60;
        maxskillcooltime1=60;
        currskillcooltime2 = 60;
        maxskillcooltime2 = 60;
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
        skill1.fillAmount = currskillcooltime1 / maxskillcooltime1;
        skill2.fillAmount = currskillcooltime2 / maxskillcooltime2;
    }
    void ManageStats()
    {
        attackText.text = "공격력: " + player.attack;
        attackSpeedText.text = "공격속도: " + player.attack_speed;
        criticalChanceText.text = "크리확률: " + player.critical_chance + "%";
        criticalDamageText.text = "크리뎀지: " + player.critical_damage * 100 + "%";
        InvincibleTimeText.text = "무적시간: " + player.InvincibleTime;
    }

    public void pressFirstStart()
    {
        player.rigid.velocity = Vector2.zero;
        Time.timeScale = 1;
        backGround.SetActive(false);
        CharacterUI.SetActive(true);
        Skill1UI.SetActive(true);
        Skill2UI.SetActive(true);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void playerDead()
    {
        DeadUI.SetActive(true);
    }
}
