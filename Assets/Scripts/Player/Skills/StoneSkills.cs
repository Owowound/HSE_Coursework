using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StoneState")]
public class StoneSkills : SkillObject
{
    [SerializeField] private float modifier;
    public override void SetActive(GameObject player)
    {
        player.GetComponent<PlayerHP>().damageModifier = modifier;
        player.GetComponent<PlayerAttack>().DamageModifier = modifier;
        isActive = true;
        Debug.Log($"Activate state {this.name}");
    }
    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<PlayerHP>().damageModifier = 1;
        player.GetComponent<PlayerAttack>().DamageModifier = 1;
        isActive = false;
        Debug.Log($"Deactivate state {this.name}");
    }
}
