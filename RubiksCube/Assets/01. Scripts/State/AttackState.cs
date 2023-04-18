using System;
using UnityEngine;

public class AttackState : State
{
    public override void OnStateEnter()
    {
        playerAnimator.ToggleAttack(true);
        playerMovement.StopImmediatly();

        playerMovement.IsActiveRotate = false;
        
        Vector3 lookTarget = playerInput.GetMouseWorldPosition();

        if(lookTarget.sqrMagnitude <= 0)
            lookTarget = transform.forward + transform.position;

        playerMovement.SetRotation(lookTarget);

        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
    }

    public override void StateUpdate()
    {
    }

    public override void OnStateExit()
    {
        playerAnimator.ToggleAttack(false);

        playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
