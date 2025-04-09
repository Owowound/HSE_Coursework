using System.Collections.Generic;
using UnityEngine;

public class NewGameScript : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private SaveLoadManager saveLoadManager;
    void Start()
    {
        Debug.Log("New Game");
        playerData.Levels = new Dictionary<StateManager.State, int>
        {
            { StateManager.State.Fire, 0 },
            { StateManager.State.Stone, 0 },
            { StateManager.State.Water, 0 },
            { StateManager.State.Lightning, 0 },
            { StateManager.State.Wind, 0 },
        };

        playerData.LocationsProgress = new Dictionary<Location, bool>
        {
        { Location.Fire, false },
        { Location.Stone, false },
        { Location.Lightning, false }
        };

        GlobalEXP.exp = 0;
        GlobalEXP.needEXP = 25;

        playerData.currentHP = 200;
        saveLoadManager.SaveData();
    }
}
