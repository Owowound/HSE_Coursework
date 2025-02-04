using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDamage : MonoBehaviour
{
    private Collider2D zone;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask playerLayer;

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
            if (collider.CompareTag("Player"))
            {
                HP player = collider.GetComponent<HP>();
                player.TakeDamage(damage);
            }
        }
    }
}
