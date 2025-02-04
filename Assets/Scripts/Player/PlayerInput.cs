using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private StateManager stateManager;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private void Start()
    {
        stateManager = GetComponent<StateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerMovement.TryJump();
            }
            if (Input.GetKeyDown(KeyCode.LeftAlt)) {
                playerMovement.TryRoll();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerAttack.TryAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                stateManager.CurrentStateObject.UseSkill(this.gameObject);
            }
            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    stateManager.ChangeState(i);
                    break;
                }
            }
        }
    }
}
