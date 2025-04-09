using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private AudioSource ambient;

    private float standartVolume;

    public bool isPaused { get; set; }

    public void PauseGame()
    {
        standartVolume = ambient.volume;
        ambient.volume = 0f;
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        ambient.volume = standartVolume;
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
