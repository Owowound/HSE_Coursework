using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningMovement : MonoBehaviour
{
    public Rigidbody2D player;
    private Rigidbody2D rb;

    private PlayerDetection detection;

    [SerializeField] private float followSpeed;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float hoverRange;
    [SerializeField] private float hoverSpeed;
    [SerializeField] private float timeBeforeOffset;

    [SerializeField] private float shootDistance;


    private float randomOffsetX;
    private float randomOffsetY;
    private int time = 0;
    private bool isOffsetIsChanged;

    private Vector2 start;

    private Rigidbody2D project = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start = rb.position;
        detection = GetComponent<PlayerDetection>();
        randomOffsetX = Random.Range(-hoverRange, hoverRange);
        randomOffsetY = Random.Range(1f, hoverHeight);
        isOffsetIsChanged = false;
    }

    void Update()
    {
        if (!isOffsetIsChanged)
        {
            StartCoroutine(WaitForOffset());
            randomOffsetX = Random.Range(-hoverRange, hoverRange);
            randomOffsetY = Random.Range(1f, hoverHeight);
        }

        if (detection.FollowingPlayer())
        {
            GoToPlayer();
        }
        else
        {
            ReturnToStart();
        }
    }
    private IEnumerator WaitForOffset()
    {
        isOffsetIsChanged = true;
        yield return new WaitForSeconds(timeBeforeOffset);
        isOffsetIsChanged = false;
    }

    private void ReturnToStart()
    {
        transform.position = Vector3.MoveTowards(transform.position, start, followSpeed * Time.deltaTime);
    }

    private void GoToPlayer()
    {
        Vector2 targetPosition = player.position + new Vector2(randomOffsetX, randomOffsetY);

        transform.position = Vector2.Lerp(transform.position, targetPosition, hoverSpeed * Time.deltaTime);
    }
}
