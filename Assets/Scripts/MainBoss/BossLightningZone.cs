using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLightningZone : MonoBehaviour
{
    private BossMovement sm;
    private BossAttack sa;
    private void Start()
    {
        sm = GetComponentInParent<BossMovement>();
        sa = GetComponentInParent<BossAttack>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InLightningZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InLightningZone = false;
        }
    }
}
