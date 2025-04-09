using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class CloasePassedLocationPrompt : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private GameObject thisPrompt;
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
        thisPrompt.SetActive(false);
    }
}
