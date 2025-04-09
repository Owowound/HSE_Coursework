using System.Collections;
using TMPro;
using UnityEngine;

public class InteractiveTorch : InteractiveObject
{
    [SerializeField]
    private int needNumberOfDefeatedEnemies;

    [SerializeField]
    private GameObject flame;

    [SerializeField]
    private GameObject enterToTemple;

    [SerializeField]
    private GameObject prompt;

    [SerializeField]
    GameObject pointed;
    public override void Interact()
    {
        if (DefeatedEnemyOnLocation.Number < needNumberOfDefeatedEnemies)
        {
            prompt.SetActive(true);
            prompt.GetComponent<TextMeshProUGUI>().text = "Вы не убили достаточное количество врагов на локации";
            StartCoroutine(ClosePrompt());
            return;
        }
        base.Interact();
        flame.SetActive(true);
        GetComponentInChildren<AudioSource>().Play();
        enterToTemple.SetActive(false);
        Destroy(pointed);
    }

    private IEnumerator ClosePrompt()
    {
        yield return new WaitForSeconds(1f);

        prompt.SetActive(false);
    }

    public override void OnPointed()
    {
        base.OnPointed();
        pointed.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        pointed.SetActive(false);
    }
}
