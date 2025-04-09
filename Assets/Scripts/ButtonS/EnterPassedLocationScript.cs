using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class EnterPassedLocationScript : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

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
        ChangingScene.CloseScene(LocationData.CurrentLocation.ToString());
    }
}
