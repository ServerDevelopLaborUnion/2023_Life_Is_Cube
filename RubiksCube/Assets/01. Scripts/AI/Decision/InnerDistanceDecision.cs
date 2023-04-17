using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField] Transform targetTrm;
    [SerializeField, Range(0f, 50f)] float distance = 10f;

    public override bool MakeDecision()
    {
        if(targetTrm == null)
            return false;

        return (Vector3.Distance(targetTrm.position, transform.position) < distance);
    }
}
