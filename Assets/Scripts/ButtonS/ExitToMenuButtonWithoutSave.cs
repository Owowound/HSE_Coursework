using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToMenuButtonWithoutSave : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += ExitToMenu;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void ExitToMenu()
    {
        ChangingScene.CloseScene("ChooseScene");
        Time.timeScale = 1f;
    }
}
