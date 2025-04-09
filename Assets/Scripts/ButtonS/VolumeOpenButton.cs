using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class SettingsOpenButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField] private SettingsCondition condition;

    public GameObject Menu;

    public GameObject volumeSlider;
    public GameObject managementText;


    public bool isActive = false;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += OpenVolumeSettings;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void OpenVolumeSettings()
    {
        volumeSlider.SetActive(true);
        managementText.SetActive(false);
        if (condition.currentCondition == Condition.None)
        {
            Menu.GetComponent<Animator>().SetTrigger("Open");
        }
        else if (condition.currentCondition == Condition.Volume)
        {
            condition.currentCondition = Condition.None;
            Menu.GetComponent<Animator>().SetTrigger("Close");
            return;
        }
        condition.currentCondition = Condition.Volume;
    }


}
