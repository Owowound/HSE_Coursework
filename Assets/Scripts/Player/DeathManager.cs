using System.Collections;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deathPrompt;

    private PlayerAttack pa;
    private PlayerMovement pm;
    private Animator animator;
    private void Start()
    {
        pa = GetComponent<PlayerAttack>();
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    public void Die()
    {
        SoundManager.PlayerDeath();
        animator.SetTrigger("Death");
        pa.BanAttack();
        pm.BanAct();
        deathPrompt.SetActive(true);
        StartCoroutine(WaitAnimation());
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(1f);

        ChangingScene.CloseScene("ChooseScene");
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(1f);

        deathPrompt.GetComponent<Animator>().SetTrigger("Open");
        StartCoroutine(SwitchScene());
    }
}
