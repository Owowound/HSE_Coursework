using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangingScene : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI textBar;

    [SerializeField]
    private Image imageBar;

    private AsyncOperation loadingScene = null;
    private string sceneName;

    private static bool isShouldOpenScene = false;

    private static ChangingScene instance;

    private bool isLoading = false;

    void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();

        if (isShouldOpenScene)
        {
            animator.SetTrigger("OpenScene");
            isShouldOpenScene = false;
        }
    }

    public static void CloseScene(string sceneName)
    {
        Debug.Log($"Opening scene {sceneName}");
        LocationData.CurrentLocation = sceneName;
        instance.sceneName = sceneName;
        instance.animator.SetTrigger("CloseScene");
    }

    private void Update()
    {
        if (isLoading)
        {
            textBar.text = (loadingScene.progress * 100).ToString() + "%";
            imageBar.fillAmount = loadingScene.progress;
        }
    }

    public void OnSceneLoaded()
    {
        isLoading = true;
        loadingScene = SceneManager.LoadSceneAsync(sceneName);
        loadingScene.allowSceneActivation = false;
        StartCoroutine(WaitForLoading());
    }

    private IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(1f);
        loadingScene.allowSceneActivation = true;
        isShouldOpenScene = true;
        isLoading = false;
    }
}
