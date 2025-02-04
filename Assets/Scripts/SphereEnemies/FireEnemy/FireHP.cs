using UnityEngine;

public class FireHP : HP
{
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject expManager;
    public override void TakeDamage(float damage)
    {
        Debug.Log("Damaging");
        damage = CalculatingDamage(damage);
        Debug.Log(currentHP);
        currentHP -= damage;
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        //sliderHP.value = currentHP;

        Instantiate(damagePrefab, transform.position, transform.rotation);

        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected override void Die()
    {
        expManager.GetComponent<EXP_Counter>().AddEXP(this.name);
        Destroy(gameObject);
    }
    protected override float CalculatingDamage(float damage)
    {
        return damage;
    }
}
