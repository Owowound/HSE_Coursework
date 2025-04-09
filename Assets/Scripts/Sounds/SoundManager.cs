using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    private static SoundManager instance;

    [Header("Player")]
    [SerializeField]
    private List<AudioClip> playerAttack;
    [SerializeField]
    private AudioClip playerChangeState;
    [SerializeField]
    private AudioClip fireSkillActivation;
    [SerializeField]
    private AudioClip stoneSkillActivation;
    [SerializeField]
    private AudioClip windSkillActivation;
    [SerializeField]
    private AudioClip lightningSkillActivation;
    [SerializeField]
    private AudioClip waterSkillActivation;

    [SerializeField]
    private AudioClip playerDamage;

    [SerializeField]
    private AudioClip playerDeath;

    [SerializeField]
    private AudioClip playerRoll;

    [SerializeField]
    private AudioClip buttonClick;


    [Header("Enemy")]
    [SerializeField]
    private AudioClip[] enemyDamage = new AudioClip[2];


    [Header("InteractiveObjects")]
    [SerializeField]
    private AudioClip portalSound;
    [SerializeField]
    private AudioClip cristallSound;

    [Header("SkillCast")]
    [SerializeField]
    private AudioClip fireballCast;
    [SerializeField]
    private AudioClip lightningCast;
    [SerializeField]
    private AudioClip waterCast;

    [Header("SkillEffects")]
    [SerializeField]
    private AudioClip fireExplosion;
    [SerializeField]
    private AudioClip waterHit;


    private void Start()
    {
        instance = this;
    }

    public static void PlayerAttack(int n)
    {
        instance.source.PlayOneShot(instance.playerAttack[n]);
    }

    public static void PlayerChangeState()
    {
        instance.source.PlayOneShot(instance.playerChangeState);
    }

    public static void FireBallCast()
    {
        instance.source.PlayOneShot(instance.fireballCast);
    }
    public static void LightningCast()
    {
        instance.source.PlayOneShot(instance.lightningCast);
    }
    public static void WaterCast()
    {
        instance.source.PlayOneShot(instance.waterCast);
    }

    public static void FireSkillActivate() 
    {
        instance.source.PlayOneShot(instance.fireSkillActivation);
    }
    public static void StoneSkillActivate()
    {
        instance.source.PlayOneShot(instance.stoneSkillActivation);
    }

    public static void WindSkillActivate()
    {
        instance.source.PlayOneShot(instance.windSkillActivation);
    }
    public static void LightningSkillActivate()
    {
        instance.source.PlayOneShot(instance.lightningSkillActivation);
    }
    public static void WaterSkillActivate()
    {
        instance.source.PlayOneShot(instance.waterSkillActivation);
    }


    public static void FireExplosion()
    {
        instance.source.PlayOneShot(instance.fireExplosion);
    }
    public static void WaterHit()
    {
        instance.source.PlayOneShot(instance.waterHit);
    }

    public static void PlayerDamage()
    {
        instance.source.PlayOneShot(instance.playerDamage);
    }
    public static void PlayerDeath()
    {
        instance.source.PlayOneShot(instance.playerDeath);
    }

    public static void EnemyDamage()
    {
        System.Random rnd = new System.Random();
        instance.source.PlayOneShot(instance.enemyDamage[rnd.Next(0, 1)]);
    }

    public static void Roll()
    {
        instance.source.PlayOneShot(instance.playerRoll);
    }

    public static void UsePortal()
    {
        instance.source.PlayOneShot(instance.portalSound);
    }
    public static void TakeCristall()
    {
        instance.source.PlayOneShot(instance.cristallSound);
    }

    public static void ButtonClick()
    {
        instance.source.PlayOneShot(instance.buttonClick);
    }
}
