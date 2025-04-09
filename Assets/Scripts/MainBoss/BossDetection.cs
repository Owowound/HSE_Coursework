using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAwake : MonoBehaviour
{
    private BossMovement sm;
    [SerializeField]
    private GameObject HpBar;
    private void Start()
    {
        sm = GetComponent<BossMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.name);
        //Debug.Log(sm.currentState);
        if (collider.CompareTag("Player") && sm.currentState == BossMovement.State.Wait)
        {
            //Debug.Log("Awake");
            sm.GolemAwake();
            HpBar.GetComponent<Animator>().SetTrigger("Activate");
            sm.currentState = BossMovement.State.Awake;
        }
    }
}
