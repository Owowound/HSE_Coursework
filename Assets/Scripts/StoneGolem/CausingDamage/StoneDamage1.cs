using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StoneDamage1 : EnemyDamage
{
    public override void CauseDamage(DebuffObject[] debuffes)
    {
        Debug.Log("Causing Damage");
        List<Collider2D> colliders = new List<Collider2D>();
        var colliderNumber = Physics2D.OverlapCollider(zone, new ContactFilter2D { layerMask = playerLayer }, colliders);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.CompareTag("Player") && collider.name == "Hitbox")
            {
                Debug.Log($"Подходит {collider.name}");
                PlayerHP player = collider.GetComponentInParent<PlayerHP>();
                player.TakeDamage(damage, DamageType.Default, debuffes);
            }
        }
    }
}
