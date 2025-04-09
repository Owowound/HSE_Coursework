using UnityEngine;

public class DebuffObject : ScriptableObject
{
    [SerializeField] private float effectDuration;
    public float EffectDuration { get { return effectDuration; } }
    protected bool isActive = false;

    public GameObject forInterface;
    
    public virtual void SetActive(GameObject player)
    {
        isActive = true;
    }
    public virtual void SetUnactive(GameObject player)
    {
        isActive = false;
    }
}
