using TMPro;
using UnityEngine;

public class FireHP : HP
{
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private GameObject expManager;
    public override void TakeDamage(float damage, DamageType damageType)
    {
        damage = CalculatingDamage(damage, damageType);
        currentHP -= damage;
        if (damage > 0)
        {
            GameObject damageObject = Instantiate(textDamagePrefab, transform.position, Quaternion.identity);
            damageObject.transform.SetParent(canvas.transform);
            damageObject.transform.localScale = Vector3.one;
            damageObject.GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
            SoundManager.EnemyDamage();

        }
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        //sliderHP.value = currentHP;

        Instantiate(damagePrefab, transform.position, Quaternion.identity);

        if (currentHP <= 0)
        {
            Die();
        }
    }
    protected override void Die()
    {
        expManager.GetComponent<PlayerHP>().AddHP(10);
        expManager.GetComponent<EXP_Counter>().AddEXP(this.name);
        Destroy(gameObject);
    }
    public override float CalculatingDamage(float damage, DamageType damageType)
    {
        if (damageType == DamageType.Fire)
        {
            return damage * 0.75f;
        }
        if (damageType == DamageType.Water)
        {
            return damage * 1.5f;
        }
        return damage;
    }
}
