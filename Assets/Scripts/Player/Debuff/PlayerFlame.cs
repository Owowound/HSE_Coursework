using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerFlame")]
public class PlayerFlame : DebuffObject
{
    [SerializeField] private float damagePerSecond;

    public override void SetActive(GameObject player)
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;
        player.GetComponent<CoroutineManager>().StartCoroutine(InFlame(player, 0));
        isActive = true;
        Debug.Log($"Activate state {this.name}");
    }

    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<SpriteRenderer>().color = Color.white;
        isActive = false;
        Debug.Log($"Activate state {this.name}");
    }

    public IEnumerator InFlame(GameObject player, int damageCounter)
    {
        yield return new WaitForSeconds(1f);

        player.GetComponent<PlayerHP>().TakeDamage(damagePerSecond, DamageType.Fire);
        damageCounter++;

        if (damageCounter < EffectDuration)
        {
            player.GetComponent<CoroutineManager>().StartCoroutine(InFlame(player, damageCounter));
        }
    }
}


