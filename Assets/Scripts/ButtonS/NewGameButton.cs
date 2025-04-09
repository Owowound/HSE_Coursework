using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class NewGameButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;


    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += StartNewGame;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void StartNewGame()
    {
        ChangingScene.CloseScene("NewGameScene");
    }
}
