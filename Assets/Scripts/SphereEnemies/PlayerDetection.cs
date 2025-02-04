using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Rigidbody2D player;

    [SerializeField] private float detectRadius;
    [SerializeField] private float followRadius;

    private bool isFollowingPlayer;


    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectRadius)
        {
            isFollowingPlayer = true;
        }
        if (distance > followRadius)
        {
            isFollowingPlayer = false;
        }
    }

    public bool FollowingPlayer()
    {
        return isFollowingPlayer;
    }
}
