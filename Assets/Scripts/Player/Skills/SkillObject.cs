using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[CreateAssetMenu(menuName = "State")]
public class SkillObject : ScriptableObject
{
    protected bool isActive = false;

    [SerializeField] protected float skillInterval;
    public float SkillInterval { get {  return skillInterval; } }
    [SerializeField] protected float lastSkill = 0;


    [SerializeField] protected PlayerData playerData;

    [SerializeField]
    protected StateManager.State stateName;

    [SerializeField] protected float damage;

    public float Damage { get { return damage; } }

    public virtual void SetActive(GameObject player)
    {
        Debug.Log($"Activate state {this.name} with level {playerData.Levels[stateName]}");
        lastSkill = Time.time - skillInterval + 0.25f;
        isActive = true;
    }

    public virtual void SetUnactive(GameObject player)
    {
        Debug.Log($"Deactivate state {this.name}");
        isActive = false;
    }

    public virtual void UseSkill(GameObject player)
    {
        Debug.Log($"{this.name} skill is used");
    }

    public virtual void UpgradeSkill()
    {
        Debug.Log($"{this.name} was upgraded");
    }
}
