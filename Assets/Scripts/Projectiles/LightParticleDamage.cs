using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LightParticleDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private bool isDamaged = false;
    [SerializeField] private string ownerTag;
    [SerializeField] private string targetTag;
    private Collider2D damageZone;

    private void Start()
    {
        damageZone = GetComponent<Collider2D>();
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.2f);

        Collider2D[] collidersInZone = Physics2D.OverlapCircleAll(transform.position, 4);
        foreach (var collider in collidersInZone)
        {
            if (collider.CompareTag(targetTag) && collider.name == "Hitbox")
            {
                collider.GetComponentInParent<PlayerHP>().TakeDamage(damage, DamageType.Lightning, null);
            }
        }
    }
}
