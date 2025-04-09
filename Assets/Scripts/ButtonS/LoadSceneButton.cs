using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadGameSceneButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
        private Scenes scene;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private GameObject prompt;
    [SerializeField]
    private Location location;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += LoadScene;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        LocationData.CurrentLocation = scene.ToString();
        ButtonIsPressed?.Invoke();
    }

    public void LoadScene()
    {
        if (playerData.LocationsProgress[location])
        {
            prompt.SetActive(true);
        }
        else
        {
            ChangingScene.CloseScene(LocationData.CurrentLocation.ToString());
        }
    }
}
