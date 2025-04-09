using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone1 : MonoBehaviour
{
    private BossMovement sm;
    private BossAttack sa;
    private BossAttackPhase2 sa2;
    private void Start()
    {
        sm = GetComponentInParent<BossMovement>();
        sa = GetComponentInParent<BossAttack>();
        sa2 = GetComponentInParent<BossAttackPhase2>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InAttack1Zone = true;
            sa2.InAttack1Zone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InAttack1Zone = false;
            sa2.InAttack1Zone = false;
        }
    }
}
