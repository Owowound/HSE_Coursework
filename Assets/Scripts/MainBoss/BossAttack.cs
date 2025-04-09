using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack : MonoBehaviour
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
    public void ChooseAttack()
    {
        if (inAttack3Zone && canAttack[2])
        {
            Attack("Attack3");
            return;
        }
        if (inAttack1Zone && canAttack[0])
        {
            Attack("Attack1");
            return;
        }
        if (canAttack[1])
        {
            Attack("Attack2");
            return;
        }
    }
    public void Attack(string number)
    {
        Vector2 direction = player.position - (Vector2)transform.position;
        sm.CanAct = false;
        animator.SetTrigger(number);
        Debug.Log(number);
        if (number == "Attack3")
        {
            StartCoroutine(StoneDash());
        }
        if (number == "Attack1")
        {
            rb.gravityScale = 5f;
        }
        StartCoroutine(WaitForAct());
    }
    public void Attack1Effect()
    {
        StartCoroutine(WaitForNextAttack(attack1Interval, 0));
        Debug.Log("1Attack1");
        damage1.CauseDamage(debuffes);
        rb.gravityScale = 0;
    }
    public void Attack2Effect()
    {
        StartCoroutine(WaitForNextAttack(attack2Interval, 1));
        Debug.Log("1Attack2");
        Vector2 spawnPosition = projectileSpawwnPosition.transform.position;

        System.Random rnd = new System.Random();
        GameObject projectile = Instantiate(stoneProjectilePrefab, spawnPosition, Quaternion.identity);

        Vector2 direction = (player.position + projectileOffset - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;
    }
    public void Attack3Effect()
    {
        StartCoroutine(WaitForNextAttack(attack3Interval, 2));
        Debug.Log("1Attack3");
        damage3.CauseDamage();
    }
    private IEnumerator StoneDash()
    {
        while (Vector2.Distance(transform.position, player.position + flightOffset) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position + flightOffset, dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator WaitForNextAttack(float time, int num)
    {
        canAttack[num] = false;
        yield return new WaitForSeconds(time);
        canAttack[num] = true;
    }
    public IEnumerator WaitForAct()
    {
        sm.CanAct = false;
        yield return new WaitForSeconds(sm.ActInterval);
        sm.CanAct = true;
    }
    private IEnumerator WaitForLightningAttack()
    {
        canLightning = false;

        yield return new WaitForSeconds(4f);

        canLightning = true;
    }
}
