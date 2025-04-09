using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHP : HP
{
    [SerializeField] private GameObject hitbox;
    private StoneMovement sm;
    private StoneAwake sa;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject expManager;


    protected override void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        sm = animator.GetComponent<StoneMovement>();
        sa = animator.GetComponent<StoneAwake>();
        rb = animator.GetComponent<Rigidbody2D>();
    }
    public override float CalculatingDamage(float damage, DamageType damageType)
    {
        return damage;
    }

    public override void TakeDamage(float damage, DamageType damageType)
    {
        SoundManager.EnemyDamage();
        base.TakeDamage(damage, damageType);
    }

    protected override void Die()
    {
        hitbox.SetActive(false);
        expManager.GetComponent<PlayerHP>().AddHP(10);
        expManager.GetComponent<EXP_Counter>().AddEXP(this.name);
        animator.SetTrigger("Death");
        rb.gravityScale = 5;

        sm.enabled = false;
        sa.enabled = false;
        isDead = true;
    }

    
}
