using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float damage;
    [SerializeField] private string ownerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ownerTag) || collision.isTrigger)
        {
            return;
        }
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && collision.name == "Hitbox")
        {
            HP playerHP = collision.gameObject.GetComponentInParent<HP>();
            //Debug.Log(damage);
            playerHP.TakeDamage(damage, DamageType.Default);
        }
        Destroy(gameObject);
    }
}
