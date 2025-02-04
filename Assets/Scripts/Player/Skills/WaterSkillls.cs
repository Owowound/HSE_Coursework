using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaterState")]
public class WaterSkillls : SkillObject
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    public override void UseSkill(GameObject player)
    {
        Debug.Log($"{lastSkill} {Time.time}");
        if (Time.time < lastSkill + skillInterval)
        {
            return;
        }
        lastSkill = Time.time;

        player.GetComponent<Animator>().SetTrigger("WaterSkill");

        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        float horizontal = playerRB.linearVelocity.x;
        player.GetComponent<PlayerMovement>().NormalizeDirection(horizontal);
        UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 spawnPosition = player.transform.Find("ProjectilesSpawn").position;

        Debug.Log($"{this.name} skill is used");
    }
}
