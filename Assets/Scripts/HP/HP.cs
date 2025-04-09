using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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

    public bool canTakeDamage = true;

    [SerializeField] protected GameObject textDamagePrefab;
    [SerializeField] protected GameObject canvas;
    protected virtual void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        //sliderHP.maxValue = maxHP;
        //sliderHP.value = currentHP;
    }

    public virtual void TakeDamage(float damage, DamageType damageType)
    {
        if (isDead)
        {
            return;
        }
        damage = CalculatingDamage(damage, damageType);
        currentHP -= damage;
        GameObject damageObject = Instantiate(textDamagePrefab, transform.position, Quaternion.identity);
        damageObject.transform.SetParent(canvas.transform);
        damageObject.transform.localScale = Vector3.one;
        damageObject.GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        SetAnimation(damage);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(float healAmount)
    {
        ;
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
    public abstract float CalculatingDamage(float damage, DamageType damageType);
}
