using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone3 : MonoBehaviour
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
            sa.InAttack3Zone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sa.InAttack3Zone = false;
        }
    }
}
