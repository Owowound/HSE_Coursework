using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassedLocations : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Location location;

    [SerializeField]
    private Sprite spritePassed;
    void Start()
    {
        if (playerData.LocationsProgress[location])
        {
            GetComponent<Image>().sprite = spritePassed;
        } else
        {
            GetComponent<Image>().color = Color.clear;
        }
    }
}
