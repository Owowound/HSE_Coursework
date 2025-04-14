using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BossAttackPhase2 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Rigidbody2D player;
    private Rigidbody2D rb;
    private BossMovement sm;
    private Animator animator;
    [SerializeField] private BossDamage1 damage1;
    private BossDamage3 damage3;

    [Header("Attack Features")]
    [SerializeField] private float attack1Interval;
    [SerializeField] private DebuffObject[] debuffes;
    [SerializeField] private float attack2Interval;
    [SerializeField] private float attack3Interval;
    [SerializeField] private float dashSpeed;

    private List<bool> canAttack = new List<bool>() { true, true, true };

    private bool canLightning = true;

    [Header("Flight offset")]
    [SerializeField] private Vector2 flightOffset;

    [Header("Projectile features")]
    [SerializeField] private GameObject stoneProjectilePrefab;
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Vector2 projectileOffset;
    [SerializeField] private GameObject projectileSpawwnPosition;
    [SerializeField] private GameObject lightningPrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;


    private bool inAttack1Zone;
    public bool InAttack1Zone
    {
        get { return inAttack1Zone; }
        set { inAttack1Zone = value; }
    }
    private bool inAttack3Zone = false;
    public bool InAttack3Zone
    {
        get { return inAttack3Zone; }
        set { inAttack3Zone = value; }
    }

    public bool InLightningZone { get; set; }
    private void Start()
    {
        sm = GetComponent<BossMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damage3 = GetComponentInChildren<BossDamage3>();
    }
    public void ChooseAttack2()
    {
        if (canAttack[0] && player.GetComponent<PlayerMovement>().isGrounded)
        {
            Teleport(0.5f);
            Attack2("Attack1");
            return;
        }
        if (canAttack[1] && player.GetComponent<PlayerMovement>().isGrounded)
        {
            Teleport(1);
            Attack2("Attack2");
            return;
        }
        if (canAttack[2])
        {
            Teleport(4);
            Attack2("Attack3");
            return;
        }
    }
    public void Attack2(string number)
    {
        Vector2 direction = player.position - (Vector2)transform.position;
        sm.CanAct = false;
        animator.SetTrigger(number);
        if (number == "Attack1")
        {
            rb.gravityScale = 5f;
        }
        StartCoroutine(WaitForAct2());
    }
    public void Attack1Effect2()
    {
        StartCoroutine(WaitForNextAttack2(attack1Interval, 0));
        damage1.CauseDamage(debuffes);
        rb.gravityScale = 0;
    }
    public void Attack2Effect2()
    {
        StartCoroutine(WaitForNextAttack2(attack2Interval, 1));
        damage3.CauseDamage();
    }
    public void Attack3Effect2()
    {
        StartCoroutine(WaitForNextAttack2(attack2Interval, 2));
        Vector2 spawnPosition = projectileSpawwnPosition.transform.position;

        System.Random rnd = new System.Random();
        GameObject projectile = Instantiate(fireProjectilePrefab, spawnPosition, Quaternion.identity);

        Vector2 direction = (player.position + projectileOffset - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;
    }
    private void Teleport(float distanceMod)
    {
        transform.position = player.position + Vector2.left * distanceMod * (UnityEngine.Random.Range(0, 2) * 2 - 1);
    }
    private IEnumerator WaitForNextAttack2(float time, int num)
    {
        canAttack[num] = false;
        yield return new WaitForSeconds(time);
        canAttack[num] = true;
    }
    public IEnumerator WaitForAct2()
    {
        sm.CanAct = false;
        yield return new WaitForSeconds(sm.ActInterval);
        sm.CanAct = true;
    }
    private IEnumerator WaitForLightningAttack2()
    {
        canLightning = false;

        yield return new WaitForSeconds(4f);

        canLightning = true;
    }

}
