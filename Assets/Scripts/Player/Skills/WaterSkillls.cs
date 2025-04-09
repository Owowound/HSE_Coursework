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
        SoundManager.WaterCast();
        lastSkill = Time.time;

        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        player.GetComponent<PlayerMovement>().BanNormalizing(0.5f);
        player.GetComponent<PlayerMovement>().NormalizeDirectionForAttack();
        player.GetComponent<Animator>().SetTrigger("WaterSkill");

        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

        float horizontal = playerRB.linearVelocity.x; // Поворот игрока и дальнейшая ротация снаряда, чтобы он смотрел в сторону курсора
        player.GetComponent<PlayerMovement>().NormalizeDirection(horizontal);
        UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 spawnPosition = player.transform.Find("WaterProjectileSpawn").position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        projectile.GetComponent<WaterProjectileBehavior>().damage = damage * (1f + (float)playerData.Levels[StateManager.State.Water] / 5f);

        Vector2 direction = (mousePosition - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;

        Debug.Log($"{this.name} skill is used");
    }

    public override void SetActive(GameObject player)
    {
        SoundManager.WaterSkillActivate();
        base.SetActive(player);
    }
}
