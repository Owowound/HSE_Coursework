using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    private List<DebuffObject> listOfDebuff = new List<DebuffObject>();
    public List<DebuffObject> ListOfDebuff { get { return listOfDebuff; } }

    public void AddDebuff(DebuffObject newDebuff)
    {
        if (listOfDebuff.Count == 0 || !listOfDebuff.Contains(newDebuff))
        {
            newDebuff.SetActive();
            listOfDebuff.Add(newDebuff);
            StartCoroutine(WaitForDebuffEnd(listOfDebuff.Count - 1));
        }
    }

    private IEnumerator WaitForDebuffEnd(int number)
    {
        float startTime = Time.time;

            yield return new WaitForSeconds(listOfDebuff[number].EffectDuration);

        listOfDebuff[number].SetUnactive();
        listOfDebuff.Remove(listOfDebuff[number]);
    }
}
