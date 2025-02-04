using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone1 : MonoBehaviour
{
    private StoneMovement sm;
    private StoneAttack sa;
    private void Start()
    {
        sm = GetComponentInParent<StoneMovement>();
        sa = GetComponentInParent<StoneAttack>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InAttack1Zone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InAttack1Zone = false;
        }
    }
}
