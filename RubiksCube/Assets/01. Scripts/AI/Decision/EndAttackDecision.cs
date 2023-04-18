using UnityEngine;

public class EndAttackDecision : AIDecision
{
    private AIAttackState attackState = null;

    public override void SetUp(Transform parentRoot)
    {
        base.SetUp(parentRoot);

        attackState = transform.parent.GetComponent<AIAttackState>();
    }

    public override bool MakeDecision()
    {
        if(attackState == null)
            return false;

        return (attackState.IsAttacking == false);
    }
}
