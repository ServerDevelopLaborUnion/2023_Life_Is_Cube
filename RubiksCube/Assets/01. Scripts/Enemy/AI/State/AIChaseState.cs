using UnityEngine;

public class AIChaseState : AIState
{
    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void StateUpdate()
    {
        navMovement.MoveToTarget(aiBrain.TargetTrm.position);

        base.StateUpdate();
    }
}
