using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    public Rigidbody2D player;
    private Rigidbody2D rb;

    private PlayerDetection detection;

    private float lastShoot;
    [SerializeField] private float shootDistance;
    [SerializeField] private float shootInteval;

    [SerializeField] private float followSpeed;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float hoverRange;
    [SerializeField] private float hoverSpeed;


    private float randomOffsetX;
    private float randomOffsetY;
    private int time = 0;
    private bool isOffsetIsChanged = true;

    private Vector2 start;

    public GameObject prefab;
    [SerializeField] private float projectileSpeed;

    private Rigidbody2D project = null;

    [SerializeField]
    private SoundManager soundManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start = rb.position;
        detection = GetComponent<PlayerDetection>();
        randomOffsetX = Random.Range(-hoverRange, hoverRange);
        randomOffsetY = Random.Range(1f, hoverHeight);
        lastShoot = Time.time;
    }

    void Update()
    {
        if (time % 30 == 0 && !isOffsetIsChanged)
        {
            randomOffsetX = Random.Range(-hoverRange, hoverRange);
            randomOffsetY = Random.Range(1f, hoverHeight);
            isOffsetIsChanged = true;
        }
        if (time % 30 != 0)
        {
            isOffsetIsChanged = false;
        }

        if (detection.FollowingPlayer())
        {
            GoToPlayer();
            TryToShoot();
        }
        else
        {
            ReturnToStart();
        }
    }

    private void ReturnToStart()
    {
        transform.position = Vector3.MoveTowards(transform.position, start, followSpeed * Time.deltaTime);
    }

    private void GoToPlayer()
    {
        time = (int)Time.time * 10;

        Vector2 targetPosition = player.position + new Vector2(randomOffsetX, randomOffsetY);

        transform.position = Vector2.Lerp(transform.position, targetPosition, hoverSpeed * Time.deltaTime);
    }

    private void TryToShoot()
    {
        float distance = Vector2.Distance(rb.position, player.position);

        if (distance <= shootDistance && lastShoot + shootInteval <= Time.time)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        SoundManager.FireBallCast();
        lastShoot = Time.time;
        Vector2 spawnPosition = transform.position;

        GameObject projectile = Instantiate(prefab, spawnPosition, Quaternion.identity);

        Vector2 direction = (player.position + offset - spawnPosition).normalized; // Рассчет направления к игроку для поворота снаряда
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;
    }
}
