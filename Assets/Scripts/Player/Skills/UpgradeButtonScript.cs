using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.Behavior;

enum ElementState
{
    Fire,
    Stone,
    Water,
    Lightning,
    Wind
};
public class UpdrageButtonScript : MonoBehaviour
{
    public Button button;
    public event System.Action ButtonIsPressed;

    [SerializeField]
    private StateManager.State state;

    [SerializeField]
    private UpgradeManager upgradeManager;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Upgrade;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void Upgrade()
    {
        upgradeManager.UpgradeWasChose(state);
    }
}
