using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class BossHP : HP
{
    private CapsuleCollider2D collider;
    private BossMovement sm;
    private BossAwake sa;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject expManager;

    private bool wasPhaseChanged = false;
    private int healCounter = 0;

    [SerializeField] private float healAmount = 50;

    protected override void Start()
    {
        currentHP = maxHP;
        sliderHP.maxValue = maxHP;
        sliderHP.value = maxHP;
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        sm = animator.GetComponent<BossMovement>();
        sa = animator.GetComponent<BossAwake>();
        rb = animator.GetComponent<Rigidbody2D>();
    }
    public override float CalculatingDamage(float damage, DamageType damageType)
    {
        return damage;
    }

    public override void TakeDamage(float damage, DamageType damageType)
    {
        if (isDead || !canTakeDamage)
        {
            return;
        }

        if (currentHP == maxHP && GetComponent<BossMovement>().currentState == BossMovement.State.Wait)
        {
            GetComponent<BossAwake>().GolemAwake();
        }

        SoundManager.EnemyDamage();
        damage = CalculatingDamage(damage, damageType);
        currentHP -= damage;
        sliderHP.value = currentHP; 
        if (!wasPhaseChanged && currentHP <= maxHP / 2)
        {
            wasPhaseChanged = true;
            GetComponent<PhaseManager>().ChangePhase();
        }
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

    private void AddHP(float hp)
    {

        currentHP = Mathf.Min(maxHP, currentHP + hp);
        sliderHP.value = currentHP;
    }

    public void Heal()
    {
        canTakeDamage = false;
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
            GetComponent<BossMovement>().CanAct = true;
            GetComponent<BossMovement>().isHealing = false;
            canTakeDamage = true;
        }
    }

    protected override void Die()
    {
        expManager.GetComponent<PlayerHP>().AddHP(10);
        expManager.GetComponent<EXP_Counter>().AddEXP(this.name);
        animator.SetTrigger("Death");
        rb.gravityScale = 5;

        sm.enabled = false;
        sa.enabled = false;
        isDead = true;

        StartCoroutine(WaitForEndGame());
    }

    private IEnumerator WaitForEndGame()
    {
        yield return new WaitForSeconds(2f);

        ChangingScene.CloseScene("EndGame");
    }
}
