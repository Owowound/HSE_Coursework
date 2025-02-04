using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float activationDistance;
    private ParticleSystem currentParticle;
    [SerializeField] private Vector3 startRotation;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance)
        {
            return;
        }
        else
        {
            return;
        }
    }

    private void DamagePlayer()
    {
        return;
    }
}