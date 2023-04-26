using System.Collections;
using System;
using UnityEngine;

public class AIAttackState : AIState
{
    [SerializeField] float attackRadius = 2f;
    [SerializeField] float attackDelay = 2f;

    private Transform damageCaster = null;
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;

    public override void SetUp(Transform root)
    {
        base.SetUp(root);

        damageCaster = root.Find("DamageCaster");
    }

    public override void OnStateEnter()
    {
        enemyAnimator.OnAnimationEventTrigger += AttackEvent;
        enemyAnimator.OnAnimationEndTrigger += AttackStateEndHandle;

        isAttacking = false;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if(isAttacking == false)
        {
            Vector3 forward = transform.forward;
            float angle = Vector3.Angle(forward, aiBrain.TargetTrm.position - transform.position);
        
            if(angle >= 10f)
            {
                Vector3 result = Vector3.Cross(forward, aiBrain.TargetTrm.position - transform.position);
                float sign = result.y > 0 ? 1 : -1;
                transform.rotation = Quaternion.Euler(0, sign * 10f * Time.deltaTime, 0) * transform.rotation;
            } else {
                navMovement.StopImmediately();

                isAttacking = true;
                enemyAnimator.ToggleAttack(true);
            }
        }
    }

    public override void OnStateExit()
    {
        enemyAnimator.OnAnimationEventTrigger -= AttackEvent;
        enemyAnimator.OnAnimationEndTrigger -= AttackStateEndHandle;
    
        isAttacking = false;
        enemyAnimator.ToggleAttack(false);
    }

    private void AttackStateEndHandle()
    {
        Debug.Log("공격 끝남");
        enemyAnimator.ToggleAttack(false);

        StartCoroutine(DelayCoroutine(attackDelay, () => isAttacking = false));
    }

    //공격
    private void AttackEvent()
    {
        bool result = Physics.SphereCast(damageCaster.position - damageCaster.forward * attackRadius, attackRadius, damageCaster.forward, out RaycastHit hit);

        if(result)
        {
            if(hit.collider.TryGetComponent<IDamageable>(out IDamageable id))
            {
                Debug.Log("공격 맞음");
                id.OnDamage(10f, hit.point, hit.normal);
            }
        }
    }

    private IEnumerator DelayCoroutine(float delay, Action callback = null)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
