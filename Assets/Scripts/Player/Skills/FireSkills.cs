using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "FireState")]
public class FireSkills : SkillObject
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    public override void UseSkill(GameObject player)
    {
        if (Time.time < lastSkill + skillInterval)
        {
            return;
        }
        SoundManager.FireBallCast();
        lastSkill = Time.time;

        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().BanNormalizing(0.5f);
        player.GetComponent<PlayerMovement>().NormalizeDirectionForAttack();
        player.GetComponent<Animator>().SetTrigger("FireSkill");

        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

        float horizontal = playerRB.linearVelocity.x; // ѕоворот игрока и дальнейша€ ротаци€ снар€да, чтобы он смотрел в сторону курсора
        player.GetComponent<PlayerMovement>().NormalizeDirection(horizontal);
        UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 spawnPosition = player.transform.Find("FireProjectileSpawn").position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        projectile.GetComponent<FirePlayerProjectileBehavior>().damage = damage * (1f + (float)playerData.Levels[StateManager.State.Fire] / 5f);

        Vector2 direction = (mousePosition - spawnPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D projRB = projectile.GetComponent<Rigidbody2D>();
        projRB.linearVelocity = direction * projectileSpeed;

        Debug.Log($"{this.name} skill is used");
    }

    public override void SetActive(GameObject player)
    {
        base.SetActive(player);
        SoundManager.FireSkillActivate();
    }
}
