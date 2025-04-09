using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int currentEXP { get; set; }

    public int currentHP = 200;

    public Dictionary<StateManager.State, int> Levels = new Dictionary<StateManager.State, int>
    {
            { StateManager.State.Fire, 0 },
            { StateManager.State.Stone, 0 },
            { StateManager.State.Water, 0 },
            { StateManager.State.Lightning, 0 },
            { StateManager.State.Wind, 0 },
    };

    public Dictionary<Location, bool> LocationsProgress = new Dictionary<Location, bool>
    {
        { Location.Fire, false },
        { Location.Stone, false },
        { Location.Lightning, false }
    };

    private void OnEnable()
    {
        currentHP = 200;
    }

    public void UpdateData(StateManager.State state)
    {
        Levels[state]++;
        Debug.Log($"Level of {state.ToString()} was upgraded");
    }

    public void PassLocation(Location location)
    {
        LocationsProgress[location] = true;
    }

    public void LoadData(GameData data)
    {
        Levels[StateManager.State.Fire] = data.fireLvl;
        Levels[StateManager.State.Stone] = data.stoneLvl;
        Levels[StateManager.State.Wind] = data.windLvl;
        Levels[StateManager.State.Lightning] = data.lightningLvl;
        Levels[StateManager.State.Water] = data.waterLvl;

        LocationsProgress[Location.Fire] = data.isFirePassed;
        LocationsProgress[Location.Stone] = data.isStonePassed;
        LocationsProgress[Location.Lightning] = data.isLightningPassed;

        currentHP = data.HP;
    }
}
