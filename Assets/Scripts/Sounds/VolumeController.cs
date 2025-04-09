using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    public SaveLoadSettings saveLoadSettings;

    public Settings settings;

    public AudioSource menuMusic;
    public AudioSource menuEffect;

    private void Start()
    {
        musicVolumeSlider.value = settings.musicVolume;
        effectVolumeSlider.value = settings.effectVolume;

        musicVolumeSlider.onValueChanged.AddListener(SaveMusicChanges);

        effectVolumeSlider.onValueChanged.AddListener(SaveEffectChanges);
    }

    private void SaveMusicChanges(float volume)
    {
        settings.musicVolume = volume;
        menuMusic.volume = volume / 100;
        saveLoadSettings.SaveSettings();
    }

    private void SaveEffectChanges(float volume)
    {
        settings.effectVolume = volume;
        menuEffect.volume = volume / 100;
        saveLoadSettings.SaveSettings();
    }
}
