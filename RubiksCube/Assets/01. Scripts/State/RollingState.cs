using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DEFINE;

public class RollingState : State
{

    public override void OnStateEnter()
    {
        playerAnimator.ToggleRolling(true);
        playerMovement.StopImmediatly();
        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
        // 무적
        // 못움직이게, 공격 못하게
        Vector3 dir = new Vector3(joyStick.lastDir.x, 0, joyStick.lastDir.y);
        playerMovement.SetMovementDirection(dir);
    }

    public override void StateUpdate()
    {

    }

    public override void OnStateExit()
    {
        playerAnimator.ToggleRolling(false);

        //playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
