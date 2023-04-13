using System;
using UnityEngine;

public class NormalState : State
{
    public override void OnStateEnter()
    {
        playerMovement.IsActiveMove = true;
        playerInput.OnMovementKeyPressed += MovementHandle;
        playerInput.OnConsumeKeyPressed += ConsumeHandle;
    }

    public override void OnStateExit()
    {
        playerInput.OnMovementKeyPressed -= MovementHandle;
        playerInput.OnConsumeKeyPressed -= ConsumeHandle;
    }

    public override void StateUpdate()
    {
    }

    private void ConsumeHandle()
    {
        stateHandler.ChangeState(StateFlags.Consuming);
    }

    private void MovementHandle(Vector3 input)
    {
        playerMovement.SetMovementDirection(input);
    }
}
