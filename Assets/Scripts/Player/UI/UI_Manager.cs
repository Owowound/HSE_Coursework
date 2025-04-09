using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerUI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private EXP_Counter expCounter;

    [SerializeField] private TextMeshProUGUI playerEXP;
    [SerializeField] private GameObject expText;
    [SerializeField] private TextMeshProUGUI playerHeals;

    private void Start()
    {
        if (LocationData.CurrentLocation == "FinalScene")
        {
            expText.SetActive(false);
        }
        expCounter = player.GetComponent<EXP_Counter>();
        UpdateEXP(0);
    }

    public void UpdateHeals(int healNum, int healMaxNum)
    {
        playerHeals.text = "Восстановлений:\n" + healNum.ToString() + "/" + healMaxNum.ToString();
    }

    public void UpdateEXP(int currentEXP)
    {
        playerEXP.text = "Текущий опыт\n" + expCounter.CurrentEXP.ToString();
    }
}
