using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip shoot;
    public static AudioClip item;
    public static AudioClip enemy_weapon_shoot;
    public static AudioClip enemy_weapon_impact;
    static AudioSource audioSrc;

    void Start()
    {
        shoot = Resources.Load<AudioClip>("shoot");
        item = Resources.Load<AudioClip>("item");
        enemy_weapon_shoot = Resources.Load<AudioClip>("enemy_weapon_shoot");
        enemy_weapon_impact = Resources.Load<AudioClip>("enemy_weapon_impact");
        audioSrc = GetComponent<AudioSource>();
    }


    public static void PlaySound(string clip)
    {
        switch (clip)
        {   
            case "shoot":
                audioSrc.PlayOneShot(shoot, 1.0f);
                break;
            case "item":
                audioSrc.PlayOneShot(item, 1.0f);
                break;
            case "enemy_weapon_shoot":
                audioSrc.PlayOneShot(enemy_weapon_shoot, 1.0f);
                break;
            case "enemy_weapon_impact":
                audioSrc.PlayOneShot(enemy_weapon_impact, 1.0f);
                break;
        }
    }
}
