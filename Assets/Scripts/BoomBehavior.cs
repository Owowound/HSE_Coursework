using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBehavior : MonoBehaviour
{
    private float lifeTime = 1;
    void Start()
    {
        SoundManager.FireExplosion();
        Destroy(gameObject, lifeTime);
    }
}
