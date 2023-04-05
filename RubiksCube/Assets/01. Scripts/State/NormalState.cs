using UnityEngine;

public class NormalState : State
{
    public override void OnStateEnter()
    {
        playerInput.OnMovementKeyPressed += MovementHandle;
    }

    public override void OnStateExit()
    {
    }

    public override void StateUpdate()
    {
    }

    private void MovementHandle(Vector2 input)
    {
        playerMovement.SetMovementVelocity(input);
    }
}
