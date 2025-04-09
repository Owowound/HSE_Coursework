using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class ManagementButton : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField] private SettingsCondition condition;

    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject managementText;
    [SerializeField]
    private GameObject volumeSlider;

    public bool isActive = false;

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
        volumeSlider.SetActive(false);
        managementText.SetActive(true);
        if (condition.currentCondition == Condition.None)
        {
            Menu.GetComponent<Animator>().SetTrigger("Open");
        }
        else if (condition.currentCondition == Condition.Management)
        {
            condition.currentCondition = Condition.None;
            Menu.GetComponent<Animator>().SetTrigger("Close");
            return;
        }
        condition.currentCondition = Condition.Management;
    }
}
