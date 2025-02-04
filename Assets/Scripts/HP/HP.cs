using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TreeEditor;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class HP : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    public float MaxHP { get { return maxHP; } }

    protected float currentHP;
    public float CurrentHP { get { return currentHP; } }

    [SerializeField] protected Slider sliderHP;
    protected Animator animator; 

    protected float currentModifier = 1;
    public float damageModifier
    {
        get { return currentModifier; }
        set { currentModifier = value; }
    }

    protected bool isDead = false;
    protected virtual void Start()
    {
        currentHP = maxHP;
        //sliderHP.maxValue = maxHP;
        //sliderHP.value = currentHP;
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        damage = CalculatingDamage(damage);
        currentHP -= damage;
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        //sliderHP.value = currentHP;
        SetAnimation(damage);
        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        animator.SetTrigger("Death");
        isDead = true;
    }
    protected void SetAnimation(float damage)
    {
        if (damage > 0)
        {
            //Debug.Log(animator);
            animator.SetTrigger("TakeDamage");
        }
    }
    protected abstract float CalculatingDamage(float damage);
}
