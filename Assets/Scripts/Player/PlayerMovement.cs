using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine.Animations;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
    private float currentSpeed;
    private float normalizedSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollingInterval;
    [SerializeField] private float rollspeedModifier;

    private bool FacingRight = true;
    public bool canNormalize { get; set; } = true;

    private bool canRoll = true;
    private bool isRolling = false;
    public bool rollState
    {
        get { return isRolling; }
        set { isRolling = value; }
    }

    private int jumpCounter = 0;

    // For Jumping
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;
    private PlayerHP playerHP;

    // Roll
    private StateManager stateManager;

    // 
    private Animator animator;
    void Start()
    {
        Debug.Log("PlayerMovement is initialized");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
        currentSpeed = moveSpeed;
        playerHP = GetComponent<PlayerHP>();
    }


    void Update()
    {
        //Debug.Log(playerHP.currentHP);
        if (rb.isKinematic)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        if (isGrounded)
        {
            jumpCounter = 0;
        }
        Movement();
        UpdateAnimation();
    }

    /// <summary>
    /// Player movement
    /// </summary>
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (canNormalize)
        {
            NormalizeDirection(horizontal);
        }
        if (!isRolling)
        {
            normalizedSpeed = currentSpeed;
        }
        rb.linearVelocity = new UnityEngine.Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
    }


    /// <summary>
    /// Check opportunity of jumping
    /// </summary>
    public void TryJump()
    {
        Debug.Log(jumpCounter);
        if (isGrounded || (stateManager.CurrentStateName == StateManager.State.Wind && jumpCounter < 1)) // Jumping
        {
            rb.linearVelocity = new UnityEngine.Vector2();
            jumpCounter++;
            Jump();
        }
    }
    /// <summary>
    /// Jumping
    /// </summary>
    void Jump()
    {
        rb.AddForce(UnityEngine.Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    /// <summary>
    /// Check opportunity for rolling
    /// </summary>
    public void TryRoll()
    {
        if (canRoll)
        {
            StartCoroutine(AfterRoll());
            Roll();
        }
    }
    /// <summary>
    /// Rolling
    /// </summary>
    private void Roll()
    {
        animator.SetTrigger("Roll");
        isRolling = true;
        currentSpeed *= rollspeedModifier;
    }
    private IEnumerator AfterRoll()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollingInterval);
        canRoll = true;
    }
    public void NormalizeSpeedAfterRoll()
    {
        currentSpeed = normalizedSpeed;
        isRolling = false;
    }
    public void NormalizeSpeedAfterWind()
    {
        currentSpeed = moveSpeed;
        isRolling = false;
    }


    /// <summary>
    /// Update animation condition
    /// </summary>
    private void UpdateAnimation()
    {
        //Debug.Log(isGrounded);
        animator.SetFloat("SpeedY", rb.linearVelocity.y);
        animator.SetBool("IsGround", isGrounded);
        animator.SetBool("Run", isGrounded && Mathf.Abs(rb.linearVelocity.x) >= 0.5f);
    }


    /// <summary>
    /// Allowing Normalize method
    /// </summary>
    public void AllowNormalizing()
    {
        canNormalize = true;
    }
    /// <summary>
    /// Normalize method
    /// </summary>
    /// <param name="horizontal"></param>
    public void NormalizeDirection(float horizontal)
    {
        UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector2 direction = (mousePosition - (UnityEngine.Vector2)transform.position).normalized;

        if (Math.Abs(horizontal) == 0 && (FacingRight == false && direction.x > 0 || FacingRight == true && direction.x < 0))
        {
            Flip();
            return;
        }

        if (FacingRight == false && horizontal > 0 || FacingRight == true && horizontal < 0)
        {
            Flip();
        }
    }
    public void NormalizeDirectionForAttack()
    {
        UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnityEngine.Vector2 direction = (mousePosition - (UnityEngine.Vector2)transform.position).normalized;

        if (FacingRight == false && direction.x > 0 || FacingRight == true && direction.x < 0)
        {
            Flip();
            return;
        }
    }

    /// <summary>
    /// Flip player
    /// </summary>
    void Flip()
    {
        FacingRight = !FacingRight;
        UnityEngine.Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}