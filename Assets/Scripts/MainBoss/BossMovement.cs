using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public enum State
    {
        Wait,
        Awake,
        Active
    }
    public State currentState { get; set; } = State.Wait;

    private Animator animator;
    [SerializeField] private Rigidbody2D player;
    private Rigidbody2D rb;
    private BossAttack stoneAttack;

    [SerializeField] private float firstShootTime;

    [SerializeField] public float movespeed;
    [SerializeField] private Vector2 flightOffset;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 maxAttitude;
    [SerializeField] private float actInterval;
    public float ActInterval
    {
        get { return actInterval; }
    }

    private bool isAwake = false;
    private bool canMove = true;
    private bool canAct = false;
    public bool CanAct
    {
        get { return canAct; }
        set { canAct = value; }
    }
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    private bool canHeal = true;

    public bool phaseIsChanging = false;
    public bool isHealing = false;

    [SerializeField] private bool isFacingRight;
    [SerializeField]
    private GameObject fireMaterial;

    private float randomOffsetX;
    private float randomOffsetY;

    [SerializeField] private float moveInterval;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stoneAttack = GetComponent<BossAttack>();
        maxAttitude = (Vector2)transform.position + flightOffset;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            randomOffsetX = Random.Range(1, 3) * (Random.Range(0, 2) * 2 - 1);
            randomOffsetY = Random.Range(0, 0.5f);
        }
        switch (currentState)
        {
            case State.Wait:
                return;
            case State.Awake:
                GolemAwake();
                break;
            case State.Active:
                GolemBehavior();
                break;
        }

    }

    public void GolemAwake()
    {
        currentState = BossMovement.State.Awake;
        Vector2 flightPosition = (Vector2)transform.position + flightOffset;
        transform.position = Vector2.Lerp(transform.position, flightPosition, movespeed * Time.deltaTime);
        if (transform.position.y >= maxAttitude.y)
        {
            currentState = State.Active;
            canAct = true;
        }
    }
    private void GolemBehavior()
    {
        if (canAct && !phaseIsChanging && !isHealing)
        {
            if (canHeal && GetComponent<PhaseManager>().phaseNum == 1)
            {
                CanAct = false;
                canHeal = false;
                isHealing = true;
                GetComponent<Animator>().SetTrigger("Ability");
                GetComponent<BossHP>().Heal();
                StartCoroutine(WaitForHeal());
                return;
            }
            rb.linearVelocity = Vector2.zero;
            Vector2 direction = player.position - (Vector2)transform.position;
            NormalizeDirection(direction);
            switch (GetComponent<PhaseManager>().phaseNum)
            {
                case 0:
                    GetComponent<BossAttack>().ChooseAttack();
                    break;
                case 1:
                    GetComponent<BossAttackPhase2>().ChooseAttack2();
                    break;
            }
            if (canMove)
            {
                MoveToPlayer();
            }
        }
    }

    public void HealIsEnd()
    {
        isHealing = false;
    }

    private IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(10f);

        canHeal = true;
    }

    private void MoveToPlayer()
    {
        Vector2 direction;
        switch (GetComponent<PhaseManager>().phaseNum) {
            case 0:
                direction = (player.position + flightOffset) - (Vector2)transform.position;
                NormalizeDirection(direction);
                transform.position = Vector2.MoveTowards(transform.position, player.position + flightOffset, movespeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitForMove()
    {
        canMove = false;

        yield return new WaitForSeconds(moveInterval);

        canMove = true;
    }

    /// <summary>
    /// Normalize direction of moving
    /// </summary>
    /// <param name="direction"></param>
    private void NormalizeDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
    }
    /// <summary>
    /// Flip enemy
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        UnityEngine.Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        Scaler = fireMaterial.transform.localScale;
        Scaler.x *= -1;
        fireMaterial.transform.localScale = Scaler;

        Scaler = GetComponentInChildren<Canvas>().transform.localScale;
        Scaler.x *= -1;
        GetComponentInChildren<Canvas>().transform.localScale = Scaler;
    }


}
