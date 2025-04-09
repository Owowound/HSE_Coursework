using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningAttack : MonoBehaviour
{
    [SerializeField] private GameObject lightningParticle;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Rigidbody2D owner;
    [SerializeField] private float activationDistance;
    [SerializeField] private float attackInterval;
    private bool canAttack = true;

    void Update()
    {
        if (Vector3.Distance(player.position, owner.position) < activationDistance && canAttack)
        {
            SoundManager.LightningCast();
            Instantiate(lightningParticle, owner.position, Quaternion.identity);
            StartCoroutine(WaitForLightningAttack());
        }
    }

    private IEnumerator WaitForLightningAttack()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackInterval);

        canAttack = true;
    }
}