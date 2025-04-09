using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class ManagementInGameButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private GameObject Menu;

    private bool isActive = false;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Management;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void Management()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
}
