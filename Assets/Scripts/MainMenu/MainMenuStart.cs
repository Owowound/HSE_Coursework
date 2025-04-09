using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    public SaveLoadSettings settings;

    private void Awake()
    {
        settings.LoadSettings();
    }
}
