using UnityEngine;

public class RollingState : State
{
    [SerializeField] float rollingSpeed = 20f;

    public override void OnStateEnter()
    {
        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;

        
        playerMovement.IsActiveMove = false;
        playerMovement.StopImmediatly();

        // 무적
        // 못움직이게, 공격 못하게
        Vector3 dir = playerInput.GetInputDirection();
        if(dir.sqrMagnitude > 0)
            playerMovement.SetMovementVelocity((Quaternion.Euler(0, 45f, 0) * dir.normalized) * rollingSpeed);
        else
            playerMovement.SetMovementVelocity(transform.forward * rollingSpeed);
        
        playerAnimator.ToggleRolling(true);
    }

    public override void StateUpdate()
    {

    }

    public override void OnStateExit()
    {
        playerAnimator.ToggleRolling(false);

        //playerMovement.IsActiveRotate = true;
        Debug.Log(playerMovement.IsActiveMove);
        playerMovement.IsActiveMove = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
