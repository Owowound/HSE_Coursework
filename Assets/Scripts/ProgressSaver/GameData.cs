using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int playerLvl;

    public int fireLvl;
    public int stoneLvl;
    public int waterLvl;
    public int lightningLvl;
    public int windLvl;


    public bool isFirePassed;
    public bool isStonePassed;
    public bool isLightningPassed;

    public int needEXP;

    public int HP;

    public GameData(Dictionary<StateManager.State, int> Levels, Dictionary<Location, bool> LocationsProgress, int currentHP)
    {
        fireLvl = Levels[StateManager.State.Fire];
        stoneLvl = Levels[StateManager.State.Stone];
        waterLvl = Levels[StateManager.State.Water];
        lightningLvl = Levels[StateManager.State.Lightning];
        windLvl = Levels[StateManager.State.Wind];

        isFirePassed = LocationsProgress[Location.Fire];
        isStonePassed = LocationsProgress[Location.Stone];
        isLightningPassed = LocationsProgress[Location.Lightning];

        playerLvl = fireLvl + stoneLvl + waterLvl + windLvl + lightningLvl;

        needEXP = GlobalEXP.needEXP;
        HP = currentHP;
    }

    public GameData() { }
}
