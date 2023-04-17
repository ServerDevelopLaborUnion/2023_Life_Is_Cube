using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    [SerializeField] AIState nextState;
    public AIState NextState => nextState;

    private List<AIDecision> decisions;

    public void SetUp(Transform parentRoot)
    {
        decisions = new List<AIDecision>();
        GetComponents<AIDecision>(decisions);

        decisions.ForEach(d => d.SetUp(parentRoot));
    }

    public bool CheckDecisions()
    {
        bool result = false;

        foreach(AIDecision d in decisions)
        {
            result = d.MakeDecision();
            if(d.IsReverse)
                result = !result;
            if(result == false)
                break;
        }

        return result;
    }
}
