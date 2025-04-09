using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private StateManager stateManager;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private InteractiveManager interactiveManager;
    private Pause pause;
    private PlayerHP playerHP;
    private HealingManager playerHeal;

    private void Start()
    {
        stateManager = GetComponent<StateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        interactiveManager = GetComponent<InteractiveManager>();
        pause = GetComponent<Pause>();
        playerHP = GetComponent<PlayerHP>();
        playerHeal = GetComponent<HealingManager>();
}

    private void Update()
    {
        if (Input.anyKeyDown && !playerHP.IsDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !pause.isPaused)
            {
                playerMovement.TryJump();
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt) && !pause.isPaused) {
                playerMovement.TryRoll();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !pause.isPaused)
            {
                playerAttack.TryAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && !pause.isPaused)
            {
                stateManager.CurrentStateObject.UseSkill(this.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.E) && !pause.isPaused)
            {
                interactiveManager.Interact();
            }
            if (Input.GetKeyDown(KeyCode.F) && !pause.isPaused)
            {
                playerHeal.Heal();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pause.isPaused)
                {
                    pause.ContinueGame();
                } else
                {
                    pause.PauseGame();
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !pause.isPaused)
                {
                    stateManager.ChangeState(i);
                    break;
                }
            }
        }
    }
}
