using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffManager : MonoBehaviour
{
    private List<DebuffObject> listOfDebuff = new List<DebuffObject>();

    private List<GameObject> listOfObjects = new List<GameObject>();
    public List<DebuffObject> ListOfDebuff { get { return listOfDebuff; } }

    [SerializeField]
    private GameObject parentGrid;

    public void AddDebuff(DebuffObject newDebuff)
    {
        if (listOfDebuff.Count == 0 || !listOfDebuff.Contains(newDebuff))
        {
            newDebuff.SetActive(this.gameObject);
            listOfDebuff.Add(newDebuff);

            Debug.Log(newDebuff.forInterface.name);
            GameObject debuff = Instantiate(newDebuff.forInterface);

            listOfObjects.Add(debuff);
            debuff.transform.SetParent(parentGrid.transform, false);
            StartCoroutine(WaitForDebuffEnd(listOfDebuff.Count - 1));
        }
    }

    private IEnumerator WaitForDebuffEnd(int number)
    {
        float startTime = Time.time;

        yield return new WaitForSeconds(listOfDebuff[number].EffectDuration);

        listOfDebuff[number].SetUnactive(this.gameObject);
        listOfDebuff.Remove(listOfDebuff[number]);

        Destroy(listOfObjects[number]);
        listOfObjects.Remove(listOfObjects[number]);
    }
}
