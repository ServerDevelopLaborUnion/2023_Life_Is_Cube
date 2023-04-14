using System;
using UnityEngine;

public class AttackState : State
{
    public override void OnStateEnter()
    {
        playerAnimator.ToggleAttack(true);
        playerMovement.StopImmediatly();

        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
    }

    public override void StateUpdate()
    {
    }

    public override void OnStateExit()
    {
        playerAnimator.ToggleAttack(false);

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
