using UnityEngine;

public class NormalState : State
{
    public override void OnStateEnter()
    {
        playerMovement.IsActiveMove = true;
        playerInput.OnMovementKeyPressed += MovementHandle;
    }

    public override void OnStateExit()
    {
        playerInput.OnMovementKeyPressed -= MovementHandle;
    }

    public override void StateUpdate()
    {
    }

    private void MovementHandle(Vector3 input)
    {
        playerMovement.SetMovementVelocity(input);
    }
}
