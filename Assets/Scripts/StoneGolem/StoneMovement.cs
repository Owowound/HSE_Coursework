using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class StoneMovement : MonoBehaviour
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
    private StoneAttack stoneAttack;

    [SerializeField] private float firstShootTime;

    [SerializeField] private float movespeed;
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
        get { return  canAct; }
        set { canAct = value; }
    }

    [SerializeField] private bool isFacingRight;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stoneAttack = GetComponent<StoneAttack>();
        maxAttitude = (Vector2)transform.position + flightOffset;
    }

    void FixedUpdate()
    {
        //Debug.Log(currentState);
        switch(currentState)
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
        if (canAct)
        {
            rb.linearVelocity = Vector2.zero;
            Vector2 direction = player.position - (Vector2)transform.position;
            NormalizeDirection(direction);
            stoneAttack.ChooseAttack();
            
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 direction = (player.position + flightOffset) - (Vector2)transform.position;
        NormalizeDirection(direction);

        transform.position = Vector2.MoveTowards(transform.position, player.position + flightOffset, movespeed * Time.deltaTime);
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
        } else
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
    }

    
}
