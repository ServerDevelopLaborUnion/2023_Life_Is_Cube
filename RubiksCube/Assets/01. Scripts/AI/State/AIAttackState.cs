using UnityEngine;

public class AIAttackState : AIState
{
    public override void OnStateEnter()
    {
        navMovement.StopImmediately();
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
