using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HP
{
    private StateManager stateManager;
    private PlayerMovement mov;

    protected override void Start()
    {
        currentHP = maxHP;
        //sliderHP.maxValue = maxHP;
        //sliderHP.value = currentHP;
        stateManager = GetComponent<StateManager>();
        mov = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    protected override float CalculatingDamage(float damage)
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
            return damage / 1.5f;
        }
    }

    protected override void Die()
    {
        animator.SetTrigger("Death");
    }
}
