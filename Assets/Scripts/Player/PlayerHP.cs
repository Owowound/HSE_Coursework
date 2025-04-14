using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class PlayerHP : HP
{
    private StateManager stateManager;
    private PlayerMovement mov;
    private DebuffManager debuffManager;

    public bool IsDead
    {
        get { return isDead; }
    }
    [SerializeField]
    private PlayerData playerData;

    protected override void Start()
    {
        maxHP = playerData.currentHP; 
        currentHP = maxHP;
        sliderHP.maxValue = maxHP;
        sliderHP.value = currentHP;
        stateManager = GetComponent<StateManager>();
        mov = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        debuffManager = GetComponent<DebuffManager>();
    }
    public void TakeDamage(float damage, DamageType damageType, DebuffObject[] debuffes)
    {
        if (isDead)
        {
            return;
        }
        damage = CalculatingDamage(damage, damageType);
        if (damage > 0)
        {
            GameObject damageObject = Instantiate(textDamagePrefab, transform.position, Quaternion.identity);
            damageObject.transform.SetParent(canvas.transform);
            damageObject.transform.localScale = Vector3.one;
            damageObject.GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
            SoundManager.PlayerDamage();
            if (debuffes != null)
            {
                foreach (var debuff in debuffes)
                {
                    debuffManager.AddDebuff(debuff);
                }
            }
        }
        currentHP -= damage;
        sliderHP.value = currentHP;
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        SetAnimation(damage);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public override void TakeDamage(float damage, DamageType damageType)
    {
        if (isDead)
        {
            return;
        }
        damage = CalculatingDamage(damage, damageType);
        currentHP -= damage;
        sliderHP.value = currentHP;
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

    public override void Heal(float healAmount)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        StartCoroutine(Heal(healAmount, 0));
    }
    public IEnumerator Heal(float healAmount, int healCounter)
    {

        yield return new WaitForSeconds(0.025f);


        AddHP(1);
        healCounter++;

        if (healCounter < healAmount)
        {
            GetComponent<CoroutineManager>().StartCoroutine(Heal(healAmount, healCounter));
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void AddHP(int hp)
    {
        currentHP = Math.Min(maxHP, currentHP + hp);
        sliderHP.value = currentHP;
    }

    public override float CalculatingDamage(float damage, DamageType damageType)
    {
        if (stateManager.CurrentStateName != StateManager.State.Stone)
        {
            if (mov.rollState)
            {
                return 0;
            }
            else
            {
                return damage;
            }
        }
        else
        {
            return damage * damageModifier;
        }
    }

    public void AddHP(float hp)
    {
        currentHP = Math.Min(maxHP, currentHP + hp);
    }

    protected override void Die()
    {
        isDead = true;
        GetComponent<DeathManager>().Die();
    }
}
