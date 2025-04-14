using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDamage : MonoBehaviour
{
    protected Collider2D zone;
    [SerializeField] protected float damage;
    [SerializeField] protected LayerMask playerLayer;

    protected virtual void Start()
    {
        zone = GetComponent<Collider2D>();
    }

    public virtual void CauseDamage()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        var colliderNumber = Physics2D.OverlapCollider(zone, new ContactFilter2D { layerMask = playerLayer }, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") && collider.name == "Hitbox")
            {
                PlayerHP player = collider.GetComponentInParent<PlayerHP>();
                player.TakeDamage(damage, DamageType.Default, null);
            }
        }
    }

    public virtual void CauseDamage(DebuffObject[] debuffes) { }
}
