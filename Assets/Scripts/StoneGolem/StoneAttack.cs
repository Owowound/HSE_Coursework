using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class StoneAttack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    private Rigidbody2D rb;
    private StoneMovement sm;
    private Animator animator;
    private StoneDamage1 damage1;
    private StoneDamage3 damage3;


    [SerializeField] private float attack1Interval;
    [SerializeField] private float attack2Interval;
    [SerializeField] private float attack3Interval;
    [SerializeField] private float dashSpeed;
    public List<bool> canAttack = new List<bool>() { true, true, true };

    [SerializeField] private Vector2 flightOffset;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Vector2 projectileOffset;
    [SerializeField] private GameObject projectileSpawwnPosition;

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
    private void Start()
    {
        sm = GetComponent<StoneMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damage1 = GetComponentInChildren<StoneDamage1>();
        damage3 = GetComponentInChildren<StoneDamage3>();
    }
    public void ChooseAttack()
    {
        if (inAttack3Zone && canAttack[2])
        {
            Attack("Attack3");
            canAttack[2] = false;
            return;
        }
        if (inAttack1Zone && canAttack[0])
        {
            Attack("Attack1");
            canAttack[0] = false;
            return;
        }
        if (canAttack[1])
        {
            Attack("Attack2");
            canAttack[1] = false;
            return;
        }
    }
    public void Attack(string number)
    {
        Vector2 direction = player.position - (Vector2)transform.position;
        sm.CanAct = false;
        animator.SetTrigger(number);
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
        damage1.CauseDamage();
        rb.gravityScale = 0;
        StartCoroutine(WaitForNextAttack(attack1Interval, 0));
    }
    public void Attack2Effect()
    {
        Vector2 spawnPosition = projectileSpawwnPosition.transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Vector2 direction = (player.position + projectileOffset - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;
        StartCoroutine(WaitForNextAttack(attack2Interval, 1));
    }
    public void Attack3Effect()
    {
        damage3.CauseDamage();
        StartCoroutine(WaitForNextAttack(attack3Interval, 2));
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
}
