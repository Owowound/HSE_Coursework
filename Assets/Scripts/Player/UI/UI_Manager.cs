using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerUI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private EXP_Counter expCounter;
    private DebuffManager debuffManager;

    [SerializeField] private TextMeshProUGUI playerEXP;
    [SerializeField] private TextMeshProUGUI textPlayerHP;
    [SerializeField] private UnityEngine.UI.Slider playerHP;
    [SerializeField] private TextMeshProUGUI playerState;
    [SerializeField] private TextMeshProUGUI playerDebuffes;

    private void Start()
    {
        playerHP.value = player.GetComponent<PlayerHP>().MaxHP;
        debuffManager = player.GetComponent<DebuffManager>();
        expCounter = player.GetComponent<EXP_Counter>();
    }
    private void Update()
    {
        playerEXP.text = expCounter.CurrentEXP.ToString();
        playerHP.value = player.GetComponent<PlayerHP>().CurrentHP;
        playerState.text = player.GetComponent<StateManager>().CurrentStateName.ToString();
        textPlayerHP.text = player.GetComponent<PlayerHP>().CurrentHP.ToString();

        playerDebuffes.text = "Debuffes:\n";
        for (int i = 0; i < debuffManager.ListOfDebuff.Count; i++)
        {
            DebuffObject debuff = debuffManager.ListOfDebuff[i];
            playerDebuffes.text += debuff.name;
        }
    }
}
