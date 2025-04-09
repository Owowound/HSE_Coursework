using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController phase1Animator;
    [SerializeField] private RuntimeAnimatorController phase2Animator;

    public int phaseNum = 0;

    private void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController = phase1Animator;
    }

    public void ChangePhase()
    {
        Debug.Log($"Change at {Time.time}");
        GetComponent<BossMovement>().phaseIsChanging = true;
        GetComponent<BossHP>().canTakeDamage = false;
        phaseNum = 1;
        GetComponent<Animator>().SetTrigger("ChangePhase");
        GetComponent<BossMovement>().movespeed = 10f;
        StartCoroutine(WaitChangingPhase());
    }

    public IEnumerator WaitChangingPhase()
    {
        yield return new WaitForSeconds(1f);

        GetComponent<BossMovement>().phaseIsChanging = false;
        GetComponent<BossHP>().canTakeDamage = true;
        GetComponent<Animator>().runtimeAnimatorController = phase2Animator;
    }
}
