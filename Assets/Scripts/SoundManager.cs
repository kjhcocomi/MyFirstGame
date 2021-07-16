using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource ads;

    public AudioClip gun;
    public AudioClip sniper;
    public AudioClip get;
    public AudioClip bow;
    public AudioClip drink;
    public AudioClip banana;
    public AudioClip changeWeapon;
    public AudioClip door;


    /*
    public void PlaySound(string action)
    {
        if (action == "gun")
        {
            ads.clip = gun;
        }
        else if (action == "sniper")
        {
            ads.clip = sniper;
        }
        else if (action == "bow")
        {
            ads.clip = bow;
        }
        else if (action == "get")
        {
            ads.clip = get;
        }
        else if (action == "drink")
        {
            ads.clip = drink;
        }
        else if (action == "banana")
        {
            ads.clip = banana;
        }
        else if (action == "changeWeapon")
        {
            ads.clip = changeWeapon;
        }
        else if (action == "door")
        {
            ads.clip = door;
        }
        ads.Play();
    }
    */
}
