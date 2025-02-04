using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform camera;
    public float parallaxEffect;

    private Vector3 lastPlayerPosition;
    void FixedUpdate()
    {
        float deltaX = camera.position.x - lastPlayerPosition.x;

        transform.position -= new Vector3(deltaX * parallaxEffect, 0, 0);

        lastPlayerPosition = camera.position;
    }
}
