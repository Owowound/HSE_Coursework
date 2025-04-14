using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine.Animations;

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

    private bool canMove = true;
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

    private Pause pause;

    private bool isWalkSoundPlaying = false;
    void Start()
    {
        Debug.Log("PlayerMovement is initialized");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
        currentSpeed = moveSpeed;
        playerHP = GetComponent<PlayerHP>();
        pause = GetComponent<Pause>();
    }

    /// <summary>
    /// Ban any act of player
    /// </summary>
    public void BanAct()
    {
        canMove = false;
        canNormalize = false;
        canRoll = false;
    }

    /// <summary>
    /// Allow act
    /// </summary>
    public void AllowAct()
    {
        canMove = true;
        canNormalize = true;
        canRoll = true;
    }

    void Update()
    {
        if (rb.isKinematic || pause.isPaused)
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
        float horizontal;
        if (!GetComponent<PlayerHP>().IsDead)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontal = 0;
            GetComponentInChildren<AudioSource>().Stop();
            isWalkSoundPlaying = false;
        }

        if (canNormalize)
        {
            NormalizeDirection(horizontal);
        }
        if (!isRolling)
        {
            normalizedSpeed = currentSpeed;
        }
        if (canMove)
        {
            rb.linearVelocity = new UnityEngine.Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
                if (Mathf.Abs(horizontal) > 0.1f && isGrounded)
                {
                    if (!isWalkSoundPlaying)
                    {
                        GetComponentInChildren<AudioSource>().Play();
                        isWalkSoundPlaying = true;
                    }
                }
                else
                {
                    GetComponentInChildren<AudioSource>().Stop();
                    isWalkSoundPlaying = false;
                }
        } else
        {
            if (isWalkSoundPlaying)
            {
                GetComponentInChildren<AudioSource>().Stop();
                isWalkSoundPlaying = false;
            }
        }
    }


    /// <summary>
    /// Check opportunity of jumping
    /// </summary>
    public void TryJump()
    {
        if (isGrounded || (stateManager.CurrentStateName == StateManager.State.Wind && jumpCounter < 1) && GetComponent<PlayerHP>())
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
        SoundManager.Jump();
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
            normalizedSpeed = currentSpeed;
            StartCoroutine(RollKD());
            StartCoroutine(RollNormalizing());
            Roll();
        }
    }
    /// <summary>
    /// Rolling
    /// </summary>
    private void Roll()
    {
        SoundManager.Roll();
        animator.SetTrigger("Roll");
        isRolling = true;
        currentSpeed *= rollspeedModifier;
    }
    private IEnumerator RollKD()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollingInterval);
        canRoll = true;
        NormalizeSpeedAfterRoll();
    }
    private IEnumerator RollNormalizing()
    {
        yield return new WaitForSeconds(0.75f);
        NormalizeSpeedAfterRoll();
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
        animator.SetFloat("SpeedY", rb.linearVelocity.y);
        animator.SetBool("IsGround", isGrounded);
        animator.SetBool("Run", isGrounded && Mathf.Abs(rb.linearVelocityX) >= 0.5f);
    }

    public IEnumerator BanNormalizing(float time)
    {
        canNormalize = false;
        yield return new WaitForSeconds(time);
        canNormalize = true;
    }
    public IEnumerator BanMoving(float time)
    {
        canMove = false;
        rb.linearVelocity = UnityEngine.Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(time);
        rb.bodyType = RigidbodyType2D.Dynamic;
        canMove = true;
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

        Scaler = GetComponentInChildren<Canvas>().transform.localScale;
        Scaler.x *= -1;
        GetComponentInChildren<Canvas>().transform.localScale = Scaler;
    }
}