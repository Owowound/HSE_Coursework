using UnityEngine;

[System.Serializable]
public class GameSettings
{
    public float musicVolume;
    public float effectVolume;

    public GameSettings(float musicVolume, float effectVolume)
    {
        this.musicVolume = musicVolume;
        this.effectVolume = effectVolume;
    }

    public GameSettings() { }
}
