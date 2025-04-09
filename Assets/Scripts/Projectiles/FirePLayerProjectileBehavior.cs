using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirePlayerProjectileBehavior : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject owner;

    public float damage;

    public float explosionRadius;
    private bool isCollided = false;

    private float spawnTime;

    [SerializeField] private string tagAttribute;
    [SerializeField] private string ownerTag;

    private bool isTriggered = false;

    [SerializeField] private PlayerData playerData;

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
                collider.GetComponentInParent<HP>().TakeDamage(damage, DamageType.Fire);
            }
        }

        Destroy(gameObject);
    }
}
