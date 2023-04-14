using System;
using UnityEngine;

public class NormalState : State
{
    private WeaponHandler weaponHandler = null;

    public override void SetUp(Transform root)
    {
        base.SetUp(root);

        weaponHandler = root.GetComponent<WeaponHandler>();
    }

    public override void OnStateEnter()
    {
        playerMovement.IsActiveMove = true;
        playerInput.OnMovementKeyPressed += MovementHandle;
        playerInput.OnConsumeKeyPressed += ConsumeHandle;
        playerInput.OnAttackKeyPressed += AttackInputHandle;
    }

    public override void OnStateExit()
    {
        playerInput.OnMovementKeyPressed -= MovementHandle;
        playerInput.OnConsumeKeyPressed -= ConsumeHandle;
        playerInput.OnAttackKeyPressed -= AttackInputHandle;
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

    private void AttackInputHandle()
    {
        if(weaponHandler.TryActiveWeapon())
            stateHandler.ChangeState(StateFlags.Attack);
    }
}
