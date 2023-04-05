using System;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    private Dictionary<StateFlags, State> stateDictionary = new Dictionary<StateFlags, State>();
    private State currentState = null;

    private void Awake()
    {
        Transform stateArchive = transform.Find("StateArchive");
        foreach(StateFlags stateFlag in Enum.GetValues(typeof(StateFlags)))
        {
            State state = stateArchive.GetComponent($"{stateFlag}State") as State;
            if(state == null)
            {
                Debug.LogWarning($"There is no script about {stateFlag} state");
                continue;
            }

            state.SetUp(transform.root);
            stateDictionary.Add(stateFlag, state);
        }
    }

    private void Start()
    {
        ChangeState(StateFlags.Normal);
    }

    public void ChangeState(StateFlags targetState)
    {
        currentState?.OnStateExit();

        currentState = stateDictionary[targetState];
        currentState?.OnStateEnter();
    }
}
