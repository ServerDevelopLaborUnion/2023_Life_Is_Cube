using System;
using UnityEditor.Build.Player;
using UnityEngine;
using static DEFINE;

public class NormalState : State
{
    private WeaponHandler weaponHandler = null;
    private SkillHandler skillHandler = null;

    public override void SetUp(Transform root)
    {
        base.SetUp(root);
       
        weaponHandler = root.GetComponent<WeaponHandler>();
        skillHandler = root.GetComponent<SkillHandler>();
    }

    public override void OnStateEnter()
    {
        playerMovement.IsActiveMove = true;
        playerInput.OnMovementKeyPressed += MovementHandle;
        playerInput.OnInteractKeyPressed += InteractHandle;
        //playerInput.OnConsumeKeyPressed += ConsumeHandle;
        playerInput.OnAttackKeyPressed += AttackInputHandle;
        playerInput.OnSpecialAttackKeyPressed += SpecialAttackInputHandle;
        playerInput.OnRollingKeyPressed -= RollingInputHandle;
    }

    public override void OnStateExit()
    {
        playerInput.OnMovementKeyPressed -= MovementHandle;
        //playerInput.OnConsumeKeyPressed -= ConsumeHandle;
        playerInput.OnInteractKeyPressed -= InteractHandle;
        playerInput.OnAttackKeyPressed -= AttackInputHandle;
        playerInput.OnSpecialAttackKeyPressed -= SpecialAttackInputHandle;
        playerInput.OnRollingKeyPressed -= RollingInputHandle;
    }

    public override void StateUpdate()
    {

    }

    private void InteractHandle()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, InteractRadius, InteractableLayer);

        float nearestDistance = 0f, tempDistance = 0f;
        int nearestIndex = 0;
        for(int i = 0; i < detectedColliders.Length; ++ i)
        {
            tempDistance = (detectedColliders[i].transform.position - transform.position).magnitude;
            if(tempDistance < nearestDistance)
            {
                nearestDistance = tempDistance;
                nearestIndex = i;
            }
        }

        if(detectedColliders[nearestIndex].TryGetComponent<IInteractable>(out IInteractable ii))
            ii?.OnInteract(stateHandler.transform);
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

    private void SpecialAttackInputHandle()
    {
        if (skillHandler.TrySkill())
            stateHandler.ChangeState(StateFlags.SpecialAttack);
    }

    private void RollingInputHandle(Vector3 dir)
    {
        
    }
}
