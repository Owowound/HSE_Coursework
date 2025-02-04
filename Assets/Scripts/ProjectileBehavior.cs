using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject owner;

    [SerializeField] private float damage;
    [SerializeField] private Collider2D DamageZone;

    public float explosionRadius;
    private bool isCollided = false;

    [SerializeField] private float lifeTime;
    private float spawnTime;

    [SerializeField] private string tagAttribute;
    [SerializeField] private string ownerTag;

    public void Initialize(GameObject owner)
    {
        this.owner = owner;
        Debug.Log(owner.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ownerTag) || collision.isTrigger)
        {
            return;
        }
        Debug.Log("COLLISION");

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        Collider2D[] collidersInZone = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var collider in collidersInZone)
        {
            if (collider.CompareTag(tagAttribute) && collider.isTrigger == false)
            {
                collider.GetComponentInParent<HP>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
