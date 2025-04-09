using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class ExitButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Exit;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
