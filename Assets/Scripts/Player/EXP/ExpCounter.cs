using System.Collections.Generic;
using UnityEngine;

public class EXP_Counter : MonoBehaviour
{
    public int currentLVL { get; set; }
    private int currentEXP;
    public int CurrentEXP
    {
        get { return currentEXP; }
    }


    [SerializeField] private int fireEXP = 45;
    [SerializeField] private int stoneGolemEXP = 65;
    [SerializeField] private int LightningEnemyEXP = 40;
    [SerializeField] private int MainBossEXP;

    [SerializeField] private PlayerUI_Manager playerUIManager;

    [SerializeField] private Dictionary<string, int> dictionary;

    private void Start()
    {
        dictionary = new Dictionary<string, int>()
        {
            { "FireEnemy", fireEXP },
            { "StoneGolem", stoneGolemEXP },
            { "LightningEnemy", LightningEnemyEXP },
            { "MainBoss", MainBossEXP }
        };
        currentEXP = 0;
    }

    public void AddEXP(string enemyName)
    {
        DefeatedEnemyOnLocation.Number++;
        currentEXP += dictionary[enemyName];
        playerUIManager.UpdateEXP(currentEXP);
    }
}
