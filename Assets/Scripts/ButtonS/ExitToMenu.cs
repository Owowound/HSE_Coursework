using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class ExitToMenuButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private SaveLoadManager saveLoadManager;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += LoadScene;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void LoadScene()
    {
        saveLoadManager.SaveData();
        ChangingScene.CloseScene("MainMenu");
    }
}
