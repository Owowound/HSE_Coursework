using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HP playerHP = collision.gameObject.GetComponent<HP>();
            Debug.Log(damage);
            playerHP.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
