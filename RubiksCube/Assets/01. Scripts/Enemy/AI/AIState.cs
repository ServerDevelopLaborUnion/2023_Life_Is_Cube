using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour, IState
{
    protected List<AITransition> transitions;
    protected AIBrain aiBrain = null;

    protected NavMovement navMovement = null;
    protected AnimatorHandler enemyAnimator = null;

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public virtual void StateUpdate()
    {
        foreach(AITransition t in transitions)
        {
            if(t.CheckDecisions())
            {
                aiBrain.ChangeState(t.NextState);
                break;
            }
        }
    }

    public virtual void SetUp(Transform root)
    {
        navMovement = root.GetComponent<NavMovement>();
        enemyAnimator = root.Find("Model").GetComponent<AnimatorHandler>();
        aiBrain = root.GetComponent<AIBrain>();

        transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(transitions);

        transitions.ForEach(t => t.SetUp(root));
    }
}
