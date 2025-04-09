using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StoneState")]
public class StoneSkills : SkillObject
{
    [SerializeField] private float modifier;

    public float Modifier { get { return modifier; } }

    public override void SetActive(GameObject player)
    {
        SoundManager.StoneSkillActivate();
        float currentModifier = modifier - (float)playerData.Levels[StateManager.State.Stone] / 25f;
        player.GetComponent<PlayerHP>().damageModifier = currentModifier;
        player.GetComponent<PlayerAttack>().DamageModifier = currentModifier;
        isActive = true;
        Debug.Log($"Activate state {this.name} with modifier {currentModifier}");
    }
    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<PlayerHP>().damageModifier = 1;
        player.GetComponent<PlayerAttack>().DamageModifier = 1;
        isActive = false;
        Debug.Log($"Deactivate state {this.name}");
    }
}
