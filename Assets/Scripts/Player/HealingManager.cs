using TMPro;
using UnityEngine;

public class HealingManager : MonoBehaviour
{
    [SerializeField] private int healMaxCount;
    private int healCount;

    [SerializeField] private float healAmount;

    [SerializeField] private PlayerUI_Manager uiManager;

    private void Start()
    {
        healCount = healMaxCount;
        uiManager.UpdateHeals(healCount, healMaxCount);
    }

    public void Heal()
    {
        if (healCount > 0)
        {
            healCount--;
            GetComponent<PlayerHP>().Heal(healAmount);
            uiManager.UpdateHeals(healCount, healMaxCount);
        }
    }
}
