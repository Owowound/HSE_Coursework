using UnityEngine;


public enum Condition
{
    Volume,
    Management,
    None
}

public class SettingsCondition : MonoBehaviour
{
    public Condition currentCondition;

    private void Start()
    {
        currentCondition = Condition.None;
    }
}
