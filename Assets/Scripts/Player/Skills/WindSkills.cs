using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "WindState")]
public class WindSkills : SkillObject
{
    [SerializeField] private float windSpeed;
    public float WindSpeed => windSpeed;


    public override void SetActive(GameObject player)
    {
        SoundManager.WindSkillActivate();
        float currentSpeed = windSpeed * (1f + (float)playerData.Levels[StateManager.State.Wind] / 50f);
        player.GetComponent<PlayerMovement>().MoveSpeed = currentSpeed;
        isActive = true;
        Debug.Log($"Activate state {this.name} with speed {currentSpeed}");
    }
    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<PlayerMovement>().NormalizeSpeedAfterWind();
        isActive = false;
        Debug.Log($"Deactivate state {this.name}");
    }
}
