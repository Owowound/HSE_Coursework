using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAwake : MonoBehaviour
{
    private StoneMovement sm;
    private void Start()
    {
        sm = GetComponentInParent<StoneMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.name);
        //Debug.Log(sm.currentState);
        if (collider.CompareTag("Player") && sm.currentState == StoneMovement.State.Wait)
        {
            sm.GolemAwake();
            sm.currentState = StoneMovement.State.Awake;
        }
    }
}
