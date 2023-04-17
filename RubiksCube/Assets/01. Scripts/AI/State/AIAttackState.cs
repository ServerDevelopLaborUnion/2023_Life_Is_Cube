using UnityEngine;

public class AIAttackState : AIState
{
    public override void OnStateEnter()
    {
        aiBrain.NavMovement.StopImmediately();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        Debug.Log("죽어랏");
    }

    public override void OnStateExit()
    {
    }
}
