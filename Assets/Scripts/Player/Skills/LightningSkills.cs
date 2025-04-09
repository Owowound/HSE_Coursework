using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "LightningState")]
public class LightningSkills : SkillObject
{
    [SerializeField] private GameObject skillLightning;

    public override void UseSkill(GameObject player)
    {
        float current_damage = damage * (1f + (float)playerData.Levels[StateManager.State.Lightning] / 5f);
        if (Time.time < lastSkill + skillInterval)
        {
            return;
        }
        SoundManager.LightningCast();
        lastSkill = Time.time;


        player.GetComponent<Animator>().SetTrigger("LightningSkill");

        Instantiate(skillLightning, player.transform.position, Quaternion.identity);

        Collider2D[] collidersInZone = Physics2D.OverlapCircleAll(player.transform.position, 4);
        foreach (var collider in collidersInZone)
        {
            if (collider.CompareTag("Enemy") && collider.isTrigger == false && collider.name == "Hitbox")
            {
                collider.GetComponentInParent<HP>().TakeDamage(current_damage, DamageType.Lightning);
            }
        }
    }

    public override void SetActive(GameObject player)
    {
        base.SetActive(player);
        SoundManager.LightningSkillActivate();
    }
}
