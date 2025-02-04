using System.Collections.Generic;
using UnityEngine;

public class EXP_Counter : MonoBehaviour
{
    private int currentEXP;
    public int CurrentEXP
    {
        get { return currentEXP; }
    }


    [SerializeField] private int fireEXP;
    [SerializeField] private int stoneGolemEXP;

    [SerializeField] private Dictionary<string, int> dictionary;

    private void Start()
    {
        dictionary = new Dictionary<string, int>()
        {
            { "FireEnemy", fireEXP },
            { "StoneGolemEXP", stoneGolemEXP }
        };
        currentEXP = 50;
    }

    public void AddEXP(string enemyName)
    {
        currentEXP += dictionary[enemyName];
    }
}
