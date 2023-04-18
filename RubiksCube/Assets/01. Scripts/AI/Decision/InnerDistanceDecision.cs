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

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject != gameObject)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
    #endif
}
