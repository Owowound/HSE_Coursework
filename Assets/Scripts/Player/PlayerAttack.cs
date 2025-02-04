using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
        private float comboPeriod;
    private int attackCounter = 0;
    private float lastAttack;
    private List<string> triggerNames = new List<string> { "Attack1", "Attack2", "Attack3" };
    [SerializeField] private List<int> listOfDamages;
    private float damageModifier = 1;
    public float DamageModifier
    { get { return damageModifier; } set { damageModifier = value; } }

    [SerializeField] private List<Collider2D> attackZones = new List<Collider2D>();
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D player;
    private PlayerMovement mov;
    private Animator animator;

    public bool canMove { get; set; }
    private bool canAttack = true;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        mov = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    public void TryAttack()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        canAttack = false;
        player.linearVelocity = Vector2.zero;
        float horizontal = Input.GetAxis("Horizontal");
        if (Time.time - lastAttack > comboPeriod || attackCounter >= 3)
        {
            attackCounter = 0;
        }

        mov.NormalizeDirectionForAttack();

        animator.SetTrigger(triggerNames[attackCounter]);
        //CauseDamage();

        lastAttack = Time.time;

        StartCoroutine(Stay());
    }
    private IEnumerator Stay()
    {
        player.linearVelocity = Vector2.zero;
        player.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(comboPeriod + 0.05f);

        player.bodyType = RigidbodyType2D.Dynamic;
    }

    public void AllowAttack()
    {
        //Debug.Log("Allowed");
        canAttack = true;
    }

    private void CauseDamage()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Получение имени анимации
        string currentAnimationName = stateInfo.IsName(triggerNames[attackCounter]) ? triggerNames[attackCounter] : "Unknown";
        switch(currentAnimationName)
        {
            case "Attack1":
                attackCounter = 1;
                break;
            case "Attack2":
                attackCounter = 2;
                break;
            case "Attack3":
                attackCounter = 3;
                break;
        }


        List<Collider2D> colliders = new List<Collider2D>();
        var colliderNumber = Physics2D.OverlapCollider(attackZones[attackCounter - 1], colliders);

        Debug.Log(attackZones[attackCounter - 1].name);
        Debug.Log(colliders.Count);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.name == "Hitbox")
            {
                collider.GetComponentInParent<HP>().TakeDamage(listOfDamages[attackCounter - 1] * damageModifier);
            }
        }
    }
}