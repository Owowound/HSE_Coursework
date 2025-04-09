using System.Collections.Generic;
using UnityEngine;

public class VolumeOnScene : MonoBehaviour
{
    public List<AudioSource> ambients;

    public List<AudioSource> effects;

    public Settings settings;

    private void Start()
    {
        foreach (var ambient in ambients)
        {
            ambient.volume = ambient.volume * (settings.musicVolume / 100);
        }

        foreach (var effect in effects)
        {
            effect.volume = effect.volume * (settings.effectVolume / 100);
        }
    }
}
