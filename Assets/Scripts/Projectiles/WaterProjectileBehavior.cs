using System.Collections;
using UnityEngine;

public class WaterProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] string targetTag;
    [SerializeField] string ownerTag;
    public float damage;

    [SerializeField] private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ownerTag) || collision.isTrigger)
        {
            return;
        }

        SoundManager.WaterHit();

        if (collision.CompareTag(targetTag) || !collision.isTrigger)
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            collision.GetComponentInParent<HP>().TakeDamage(damage, DamageType.Water);
            ProjectileDestroy();
        }
    }

    public void ProjectileDestroy()
    {
        Destroy(this.gameObject);
    }
}
