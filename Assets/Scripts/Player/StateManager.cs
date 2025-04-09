using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        None,
        Fire,
        Stone,
        Wind,
        Lightning,
        Water
    }
    [SerializeField] private List<SkillObject> listOfSkills = new List<SkillObject>(5);
    private List<KeyValuePair<State, SkillObject>> listOfStates;


    private KeyValuePair<State, SkillObject> currentState;
    public State CurrentStateName
    {
        get { return currentState.Key; }
    }
    public SkillObject CurrentStateObject
        {
        get {return currentState.Value; }
        }

    [SerializeField]
    private List<GameObject> interfaces = new List<GameObject>(5);

    private int currentStateId = 5;
    void Start()
    {
        Debug.Log("StateManager is initialized");
        InitializeStates();
    }

    private void InitializeStates()
    {
        listOfStates = new List<KeyValuePair<State, SkillObject>>
        {
            new KeyValuePair<State, SkillObject>(State.Fire, listOfSkills[0]),
            new KeyValuePair<State, SkillObject>(State.Stone, listOfSkills[1]),
            new KeyValuePair<State, SkillObject>(State.Wind, listOfSkills[2]),
            new KeyValuePair<State, SkillObject>(State.Lightning, listOfSkills[3]),
            new KeyValuePair<State, SkillObject>(State.Water, listOfSkills[4]),
            new KeyValuePair<State, SkillObject>(State.None, listOfSkills[5]),
        };
        currentState = listOfStates[5];
    }

    public void ChangeState(int newStateId)
    {
        interfaces[currentStateId].SetActive(false);
        interfaces[newStateId].SetActive(true);

        currentState.Value.SetUnactive(this.gameObject);
        currentState = listOfStates[newStateId];
        currentState.Value.SetActive(this.gameObject);
        currentStateId = newStateId;
    }


}
