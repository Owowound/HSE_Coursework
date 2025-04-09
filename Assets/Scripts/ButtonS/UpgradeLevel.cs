using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UpgradeLevel : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;
    [SerializeField]
        private PlayerData player;
    [SerializeField]
        private StateManager.State state;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Upgrade;
    }

    private void ClickButton()
    {
        ButtonIsPressed?.Invoke();
    }

    private void Upgrade()
    {
        player.UpdateData(state);
    }
}
