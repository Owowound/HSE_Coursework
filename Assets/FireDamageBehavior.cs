using UnityEngine;

public class FireDamageBehavior : MonoBehaviour
{
    private float lifetime = 2f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
