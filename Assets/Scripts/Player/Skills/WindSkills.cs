using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "WindState")]
public class WindSkills : SkillObject
{
    [SerializeField] private float windSpeed;
    public float WindSpeed => windSpeed;

    public override void SetActive(GameObject player)
    { 
        player.GetComponent<PlayerMovement>().MoveSpeed = windSpeed;
        isActive = true;
        Debug.Log($"Activate state {this.name}");
    }
    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<PlayerMovement>().NormalizeSpeedAfterWind();
        isActive = false;
        Debug.Log($"Deactivate state {this.name}");
    }
}
