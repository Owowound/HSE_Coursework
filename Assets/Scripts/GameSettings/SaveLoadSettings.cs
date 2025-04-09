using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSettings : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;

    [SerializeField] private Settings settings;

    [SerializeField] private SettingsCondition condition;

    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "GameSettings.dat");
    }

    public void SaveSettings()
    {
        GameSettings data = new GameSettings(musicVolumeSlider.value, effectVolumeSlider.value);

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public void LoadSettings()
    {
        GameSettings data = new GameSettings();
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as GameSettings;
            }
        }
        else
        {
            Debug.Log("Файл не найден");
        }

        settings.LoadSettings(data);
    }
}
