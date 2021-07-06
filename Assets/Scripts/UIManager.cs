using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider PlayerHPbar;
    public Slider PlayerShieldbar;
    public Text HPText;
    public Text ShieldText;

    public Player player;

    void Start()
    {
        PlayerHPbar.maxValue = player.max_health;
        PlayerHPbar.minValue = 0;
        PlayerShieldbar.maxValue = player.max_shield;
        PlayerShieldbar.minValue = 0;
    }

    void Update()
    {
        ManagePlayerHP();
        ManagePlyaerShield();
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

}
