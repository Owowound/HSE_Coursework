using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStun")]
public class PlayerStun : DebuffObject
{
    public override void SetActive(GameObject player)
    {
        player.GetComponent<Animator>().SetBool("IsStunned", true);
        player.GetComponent<PlayerMovement>().BanAct();
        player.GetComponent<PlayerAttack>().BanAttack();
        isActive = true;
        Debug.Log($"Activate state {this.name}");
    }

    public override void SetUnactive(GameObject player)
    {
        player.GetComponent<Animator>().SetBool("IsStunned", false);
        player.GetComponent<PlayerMovement>().AllowAct();
        player.GetComponent<PlayerAttack>().AllowAttack();
        isActive = false;
        Debug.Log($"Activate state {this.name}");
    }
}
