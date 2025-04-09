using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireEnemyProjectileBehavior : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject owner;

    public float damage;
    [SerializeField] private Collider2D DamageZone;

    public float explosionRadius;
    private bool isCollided = false;

    [SerializeField] private float lifeTime;
    private float spawnTime;

    [SerializeField] private string tagAttribute;
    [SerializeField] private string ownerTag;

    private bool isTriggered = false;

    [SerializeField] private DebuffObject debuff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ownerTag) || collision.isTrigger || isTriggered)
        {
            return;
        }
        isTriggered = true;

        GameObject boom = Instantiate(explosionPrefab, transform.position, transform.rotation);

        Collider2D[] collidersInZone = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var collider in collidersInZone)
        {
            if (collider.CompareTag(tagAttribute) && collider.name == "Hitbox")
            {
                collider.GetComponentInParent<PlayerHP>().TakeDamage(damage, DamageType.Fire, new DebuffObject[1] { debuff });
            }
        }

        Destroy(gameObject);
    }
}
