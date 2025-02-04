using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TEST : MonoBehaviour
{
    private DebuffManager dm;

    [SerializeField] private DebuffObject debuff;

    private bool canTest = true;

    private void Start()
    {
        dm = GetComponent<DebuffManager>();
    }

    private void Update()
    {
        Debug.Log(canTest);
        if (canTest)
        {
            Debug.Log(dm.name);
            dm.AddDebuff(debuff);
            StartCoroutine(WaitForTest());
        }
    }

    private IEnumerator WaitForTest()
    {
        canTest = false;

        yield return new WaitForSeconds(4);

        canTest = true;

    }

}
