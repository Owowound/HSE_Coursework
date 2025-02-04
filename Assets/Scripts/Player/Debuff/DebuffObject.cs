using UnityEngine;

public class DebuffObject : ScriptableObject
{
    [SerializeField] private float effectDuration;
    public float EffectDuration { get { return effectDuration; } }
    protected bool isActive = false;
    
    public virtual void SetActive()
    {
        isActive = true;
    }
    public virtual void SetUnactive()
    {
        isActive = false;
    }
}
