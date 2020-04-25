﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Player : MonoBehaviour
{
    public int maxHealth = 10;
    public int maxShield = 10;
    protected int actualHealth;
    protected int actualShield;
    public float damageImmunityTime=3;
    protected float damageTimeLeft=0;

    public Vector3 spawnPoint;

    public int ActualHealth { get => actualHealth; }
    public int ActualShield { get => actualShield; }

    //Blinking and sprite management
    public float blinkingGapS = 0.2f;
    float blinkingleft;
    public Color BlinkingColor;
    protected SpriteRenderer sprite;
    bool ended;

    protected void Start()
    {
        spawnPoint = transform.position;

        actualHealth = maxHealth;
        //actualShield = maxShield;
        actualShield = 0;

        sprite=GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        if (damageTimeLeft > 0)
        {
            damageTimeLeft -= Time.deltaTime;
            if (blinkingleft > 0)
                blinkingleft -= Time.deltaTime;
            else
            {
                ChangeBlink();
                blinkingleft = blinkingGapS;
            }
        }
        else if (!ended)
        {
            ended = true;
            sprite.color = Color.white;
        }
    }


    public void Damage(int amount)
    {
        if (amount < 1) return;
        if (damageTimeLeft > 0) return;
        else if (actualShield > 0)
        {
            int oldShield = actualShield;
            actualShield = Mathf.Clamp(actualShield - amount, 0, maxShield);
            if (actualShield == 0)
                Damage(Mathf.Abs(oldShield - amount));
        }
        else
        {
            actualHealth = Mathf.Clamp(actualHealth - amount, 0, maxHealth);
            if(actualHealth == 0)
            {
                Die();
            }
        }
        Debug.Log($"lifePlayer:{actualHealth}/{maxHealth}");
        Debug.Log($"shieldPlayer:{actualShield}/{maxShield}");
        damageTimeLeft = damageImmunityTime;
        blinkingleft = blinkingGapS;
        ChangeBlink();
        ended = false;
    }

    public void HealLife(int amount)
    {
        if (amount < 1) return;
        actualHealth = Mathf.Clamp(actualHealth + amount, 0, maxHealth);
        Debug.Log($"Curato lifePlayer:{actualHealth}/{maxHealth}");
    }

    public void HealArmor(int amount)
    {
        if (amount < 1) return;
        actualShield = Mathf.Clamp(actualShield + amount, 0, maxShield);
        Debug.Log($"Shieldato shieldPlayer:{actualShield}/{maxShield}");
    }

    public void Die()
    {
        actualHealth = maxHealth;
        transform.position = spawnPoint;
    }

    #region privates

    void ChangeBlink()
    {
        if (sprite.color == new Color(255, 255, 255))
            sprite.color = BlinkingColor;
        else
            sprite.color = new Color(255, 255, 255);
    }

    #endregion
}
