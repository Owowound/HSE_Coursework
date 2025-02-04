using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHP : HP
{
    private CapsuleCollider2D collider;
    private StoneMovement sm;
    private StoneAwake sa;
    private Rigidbody2D rb;

    protected override void Start()
    {
        currentHP = maxHP;
        //sliderHP.maxValue = maxHP;
        //sliderHP.value = currentHP;
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        sm = animator.GetComponent<StoneMovement>();
        sa = animator.GetComponent<StoneAwake>();
        rb = animator.GetComponent<Rigidbody2D>();
    }
    protected override float CalculatingDamage(float damage)
    {
        return damage;
    }

    protected override void Die()
    {
        animator.SetTrigger("Death");
        rb.gravityScale = 5;

        sm.enabled = false;
        sa.enabled = false;
        isDead = true;
    }

    
}
