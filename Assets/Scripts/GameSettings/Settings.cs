using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class Settings : ScriptableObject
{
    public float musicVolume;
    public float effectVolume;

    public void LoadSettings(GameSettings settings)
    {
        musicVolume = settings.musicVolume;
        effectVolume = settings.effectVolume;
    }
}
