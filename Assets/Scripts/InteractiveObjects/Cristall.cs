using UnityEngine;

public class Cristall : InteractiveObject
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject pointed;
    void Start()
    {
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        Debug.Log(particleSystem.name);
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }

    public override void Interact()
    {
        base.Interact();
        SoundManager.TakeCristall();
        player.GetComponent<CristallCounter>().isCristallCollected = true;
        this.gameObject.SetActive(false);
        Destroy(pointed);
    }

    public override void OnPointed()
    {
        base.OnPointed();
        pointed.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        pointed.SetActive(false);
    }
}
