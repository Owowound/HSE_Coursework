using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
        private float comboPeriod;
    private int attackCounter = 0;
    [SerializeField]
        private float movePeriod;
    private float attackTime;
    private List<string> triggerNames = new List<string> { "Attack1", "Attack2", "Attack3" };
    [SerializeField] 
        private List<int> listOfDamages;
    private float damageModifier = 1;
    public float DamageModifier
    { get { return damageModifier; } set { damageModifier = value; } }

    [SerializeField]
        private List<float> attackIntervals;
    [SerializeField] 
        private List<Collider2D> attackZones = new List<Collider2D>();
    [SerializeField] 
        private LayerMask enemyLayer;

    private Rigidbody2D player;
    private PlayerMovement mov;
    private Animator animator;

    public bool canMove { get; set; }
    private bool canAttack = true;
    private bool isComboActive = false;
    private float lastAttack = -10;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        mov = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    public void BanAttack()
    {
        canAttack = false;
    }

    public void AllowAttack()
    {
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
        float horizontal = Input.GetAxis("Horizontal");

        //Debug.Log($"{attackCounter} {isComboActive}");
        if (attackCounter >= 3 || lastAttack + comboPeriod < Time.time)
        {
            attackCounter = 0;
        }
        lastAttack = Time.time;
        mov.NormalizeDirectionForAttack();
        StartCoroutine(mov.BanNormalizing(movePeriod));
        StartCoroutine(mov.BanMoving(movePeriod));

        animator.SetTrigger(triggerNames[attackCounter]);
        attackCounter++;
        StartCoroutine(WaitAttack(attackIntervals[attackCounter - 1]));

        SoundManager.PlayerAttack(attackCounter - 1);
    }

    private void CauseDamage()
    {
        if (attackCounter > 0)
        {
            List<Collider2D> colliders = new List<Collider2D>();
            var colliderNumber = Physics2D.OverlapCollider(attackZones[attackCounter - 1], colliders);

            float damage;
            if (damageModifier != 1)
            {
                Debug.Log(damageModifier);
                damage = listOfDamages[attackCounter - 1] * (1.8f - damageModifier);
            }
            else
            {
                 damage = listOfDamages[attackCounter - 1] * damageModifier;
            }

            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.name);
                if (collider.name == "Hitbox")
                {
                    collider.GetComponentInParent<HP>().TakeDamage(damage, DamageType.Default);
                }
            }
        }
    }

    public IEnumerator WaitAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
    public IEnumerator ComboWaiter()
    {
        isComboActive = true;
        yield return new WaitForSeconds(comboPeriod);
        isComboActive = false;
    }
}