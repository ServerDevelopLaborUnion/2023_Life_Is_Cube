using System;
using UnityEngine;

public class AttackState : State
{
    private AIBrain targetEnemy = null;

    private void Update()
    {
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

        AIBrain newTarget = target.GetComponent<AIBrain>();
        if(newTarget != null && targetEnemy != newTarget)
        {
            if(targetEnemy != null)
                targetEnemy.IsFocused = false;
            targetEnemy = newTarget;
            targetEnemy.IsFocused = true;
        }
    }

    public override void OnStateEnter()
    {
        playerAnimator.ToggleAttack(true);
        playerMovement.StopImmediatly();

        if (targetEnemy != null)
        {
            Vector3 lookTarget = targetEnemy.transform.position;
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
