using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalLevelButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private bool canPlayerEndGame;

    private void Start()
    {
        if (!playerData.LocationsProgress[Location.Fire] || !playerData.LocationsProgress[Location.Stone] || !playerData.LocationsProgress[Location.Lightning])
        {
            if (!canPlayerEndGame)
            {
                button.gameObject.SetActive(false);
            }
        }
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += GoToFinalLevel;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void GoToFinalLevel()
    {
        ChangingScene.CloseScene("FinalScene");
    }
}
