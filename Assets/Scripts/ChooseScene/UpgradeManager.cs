using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    private float currentEXP;
    [SerializeField]
    private GameObject updateWindow;
    [SerializeField]
    private GameObject chooseWindow;

    [SerializeField]
    private TextMeshProUGUI current;
    [SerializeField]
    private TextMeshProUGUI need;

    [SerializeField]
    private List<SkillObject> skillObjects = new List<SkillObject>(5);

    [SerializeField]
    private List<TextMeshProUGUI> levels = new List<TextMeshProUGUI>(5);


    [SerializeField]
    private PlayerData playerData;


    [SerializeField] SkillObject fireObject;
    [SerializeField] StoneSkills stoneObject;
    [SerializeField] SkillObject lightningObject;
    [SerializeField] SkillObject windObject;
    [SerializeField] SkillObject waterObject;
    private void Start()
    {
        currentEXP = GlobalEXP.exp;
        GlobalEXP.exp = 0;
        if (currentEXP >= GlobalEXP.needEXP)
        {
            ChooseUpgrade();
        }
    }

    private void FixedUpdate()
    {

        levels[0].text = (fireObject.Damage * (1 + (playerData.Levels[StateManager.State.Fire] / 5f))).ToString() + " урона от способности";
        levels[1].text = "-" + ((1 - (stoneObject.Modifier - playerData.Levels[StateManager.State.Stone] / 25f)) * 100).ToString() + "% " + " к получаемому урону; " +
                          "+" + (playerData.Levels[StateManager.State.Stone] / 25f * 100).ToString() + "%" + " к наносимому урону мечом ";
        levels[2].text = "+" + (((5f * (1 + playerData.Levels[StateManager.State.Wind] / 50f)) / 4f - 1f) * 100).ToString() + "%" + " к скорости передвижения";
        levels[3].text = (lightningObject.Damage * (1 + (playerData.Levels[StateManager.State.Lightning] / 5f))).ToString() + " урона от способности";
        levels[4].text = (waterObject.Damage * (1 + (playerData.Levels[StateManager.State.Water] / 5f))).ToString() + " урона от способности";
        current.text = "Текущий опыт:\n" + currentEXP.ToString();
        need.text = "Нужно для получения уровня:\n" + GlobalEXP.needEXP.ToString();
    }

    private void ChooseUpgrade()
    {
        updateWindow.SetActive(true);
        chooseWindow.SetActive(false);
    }

    public void UpgradeWasChose(StateManager.State state)
    {
        currentEXP -= GlobalEXP.needEXP;
        GlobalEXP.needEXP += 50;
        playerData.currentHP += 15;
        playerData.UpdateData(state);
        switch (state)
        {
            case StateManager.State.Fire:
                skillObjects[0].UpgradeSkill();
                break;
            case StateManager.State.Stone:
                skillObjects[1].UpgradeSkill();
                break;
            case StateManager.State.Wind:
                skillObjects[2].UpgradeSkill();
                break;
            case StateManager.State.Lightning:
                skillObjects[3].UpgradeSkill();
                break;
            case StateManager.State.Water:
                skillObjects[4].UpgradeSkill();
                break;
        }
        if (currentEXP >= GlobalEXP.needEXP)
        {
            ChooseUpgrade();
        }
        else
        {
            updateWindow.SetActive(false);
            chooseWindow.SetActive(true);
        }
    }
}
