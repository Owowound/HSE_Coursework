using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClosePauseButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Pause pause;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Continue;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void Continue()
    {
        pause.ContinueGame();
    }
}
