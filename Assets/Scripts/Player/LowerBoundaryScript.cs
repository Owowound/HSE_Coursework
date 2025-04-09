using UnityEngine;

public class LowerBoundaryScript : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float lowestPos;

    void Update()
    {
        if (player.transform.position.y <= lowestPos)
        {
            player.GetComponent<PlayerHP>().TakeDamage(100000, DamageType.Default, null);
        }
    }
}
