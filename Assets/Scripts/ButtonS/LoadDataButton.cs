using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadButton : MonoBehaviour
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

        if (!File.Exists(saveLoadManager.FilePath))
        {
            button.interactable = false;
            button.GetComponent<Image>().color = Color.grey;
        }
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void LoadScene()
    {
        saveLoadManager.LoadData();
        ChangingScene.CloseScene("ChooseScene");
    }
}
