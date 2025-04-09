using System.Collections;
using TMPro;
using UnityEngine;

public class PortalScript : InteractiveObject
{
    [SerializeField]
        private Scenes scene;
    [SerializeField]
        private Rigidbody2D player;
    [SerializeField]
        private float activateDistance;

    [SerializeField]
    private float interactDist;

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Location locationName;

    [SerializeField]
    private GameObject prompt;

    public override void Interact()
    {
        if (Vector2.Distance(player.position, transform.position) <= interactDist)
        {
            if (player.GetComponent<CristallCounter>().isCristallCollected)
            {
                SoundManager.UsePortal();
                if (!playerData.LocationsProgress[locationName])
                {
                    GlobalEXP.exp = player.GetComponent<EXP_Counter>().CurrentEXP;
                }
                DefeatedEnemyOnLocation.Number = 0;
                playerData.PassLocation(locationName);
                LocationData.CurrentLocation = scene.ToString();
                ChangingScene.CloseScene(LocationData.CurrentLocation);
            } else
            {
                prompt.SetActive(true);
                prompt.GetComponent<TextMeshProUGUI>().text = "Вы не собрали кристалл";
                StartCoroutine(CloseWindow());
            }
        }
    }

    private IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(1f);

        prompt.SetActive(false);
    }
}
