using TMPro;
using UnityEngine;

public class LightningHP : HP
{

    [SerializeField] private GameObject expManager;
    public override float CalculatingDamage(float damage, DamageType damageType)
    {
        if (damageType == DamageType.Lightning)
        {
            return damage / 1.5f;
        }
        if (damageType == DamageType.Water)
        {
            return damage * 1.25f;
        }
        return damage;
    }
    public override void TakeDamage(float damage, DamageType damageType)
    {
        damage = CalculatingDamage(damage, damageType);
        currentHP -= damage;
        if (damage > 0)
        {
            GameObject damageObject = Instantiate(textDamagePrefab, transform.position, Quaternion.identity);
            Debug.Log(damageObject.name);
            damageObject.transform.SetParent(canvas.transform);
            damageObject.transform.localScale = Vector3.one;
            damageObject.GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
            SoundManager.EnemyDamage();
        }
        Debug.Log($"{transform.name} is took {damage} damages. Now current HP is {currentHP}");
        GetComponent<Animator>().SetTrigger("TakeDamage");
        //sliderHP.value = currentHP;

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
}
