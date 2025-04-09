using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenStatsInChoosingMenu : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;

    [SerializeField]
    private GameObject Menu;

    private bool isActive = false;

    [SerializeField]
    private TextMeshProUGUI fireValue;
    [SerializeField]
    private TextMeshProUGUI stoneValue;
    [SerializeField]
    private TextMeshProUGUI windValue;
    [SerializeField]
    private TextMeshProUGUI lightningValue;
    [SerializeField]
    private TextMeshProUGUI waterValue;
    [SerializeField]
    private TextMeshProUGUI HP;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField] SkillObject fireObject;
    [SerializeField] StoneSkills stoneObject;
    [SerializeField] SkillObject windObject;
    [SerializeField] SkillObject lightningObject;
    [SerializeField] SkillObject waterObject;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += Stats;
    }

    private void ClickButton()
    {
        SoundManager.ButtonClick();
        ButtonIsPressed?.Invoke();
    }

    public void Stats()
    {
        if (!isActive)
        {
            ChangeStats();
            Menu.GetComponent<Animator>().SetTrigger("Activate");
            isActive = true;
        }
        else
        {
            Menu.GetComponent<Animator>().SetTrigger("Deactivate");
            isActive = false;
        }
    }

    private void ChangeStats()
    {
        fireValue.text = (fireObject.Damage * (1 + (playerData.Levels[StateManager.State.Fire] / 5f))).ToString() + " урона от способности";
        stoneValue.text = "-" + ((1 - (stoneObject.Modifier - playerData.Levels[StateManager.State.Stone] / 25f)) * 100).ToString() + "% " + " к получаемому урону; " + 
                          "+" + (playerData.Levels[StateManager.State.Stone] / 25f * 100).ToString() + "%" + " к наносимому урону мечом ";
        windValue.text = "+" + (((5f * (1 + playerData.Levels[StateManager.State.Wind] / 50f)) / 4f - 1f) * 100).ToString() + "%" + " к скорости передвижения";
        lightningValue.text = (lightningObject.Damage * (1 + (playerData.Levels[StateManager.State.Lightning] / 5f))).ToString() + " урона от способности";
        waterValue.text = (waterObject.Damage * (1 + (playerData.Levels[StateManager.State.Water] / 5f))).ToString() + " урона от способности";

        HP.text = playerData.currentHP.ToString();
    }
}

