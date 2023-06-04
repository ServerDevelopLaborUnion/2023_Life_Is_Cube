using UnityEngine;

public class AttackState : State
{
    private AIBrain targetEnemy = null;
    private Transform focusBorder = null;
    
    private void Awake()
    {
        focusBorder = transform.parent.Find("FocusBorder");
    }

    private void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 10000f, DEFINE.EnemyLayer);
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

        AIBrain newTarget = target?.GetComponent<AIBrain>();
        if(newTarget != null && targetEnemy != newTarget)
        {
            if(targetEnemy != null)
                targetEnemy.IsFocused = false;
            targetEnemy = newTarget;
            targetEnemy.IsFocused = true;
        }

        if(targetEnemy != null && targetEnemy.IsDead == false)
        {
            focusBorder.gameObject.SetActive(true);

            Vector3 dir = targetEnemy.transform.position - transform.position;
            dir.y = 0;
            focusBorder.rotation = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(90f, 0, 0);
        }
        else
            focusBorder.gameObject.SetActive(false);
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

        //playerMovement.IsActiveRotate = true;

        playerAnimator.OnAnimationEndTrigger -= OnAnimationEndHandle;
    }

    private void OnAnimationEndHandle()
    {
        playerAnimator.ToggleAttack(false);
        stateHandler.ChangeState(StateFlags.Normal);
    }
}
