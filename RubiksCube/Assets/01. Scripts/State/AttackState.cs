using System;
using UnityEngine;

public class AttackState : State
{
    public override void OnStateEnter()
    {
        playerAnimator.ToggleAttack(true);
        playerMovement.StopImmediatly();

        Collider[] enemies = Physics.OverlapSphere(transform.position, 100f, DEFINE.EnemyLayer);
        Transform target = null;
        float nearDistance = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearDistance)
            {
                nearDistance = distance;
                target = enemy.transform;
            }
        }

        if (target != null)
        {
            Vector3 lookTarget = target.position;
            //playerMovement.IsActiveRotate = false;

            if (lookTarget.sqrMagnitude <= 0)
                lookTarget = transform.forward + transform.position;

            playerMovement.SetRotationImmediatly(lookTarget);
        }


        playerAnimator.OnAnimationEndTrigger += OnAnimationEndHandle;
    }

    public override void StateUpdate()
    {
    }

    public override void OnStateExit()
    {
        playerAnimator.ToggleAttack(false);

        //playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
