using System;
using UnityEngine;

public class SpecialAttack2State : State
{
    public override void OnStateEnter()
    {
        playerAnimator.ToggleSpecialAttack(true);
        playerMovement.StopImmediatly();

        // playerMovement.IsActiveRotate = false;

        // Vector3 lookTarget = playerInput.GetMouseWorldPosition();

        // if(lookTarget.sqrMagnitude <= 0)
        //     lookTarget = transform.forward + transform.position;

        // playerMovement.SetRotation(lookTarget);

        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
    }

    public override void StateUpdate()
    {

    }

    public override void OnStateExit()
    {

        //playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        playerAnimator.ToggleAttack(false);
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
