using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{
    private NavMeshAgent navAgent = null;
    private AnimatorHandler agentAnimator = null;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        agentAnimator = transform.Find("Model").GetComponent<AnimatorHandler>();
    }

    private void Update()
    {
        agentAnimator.SetSpeed(IsArrived() ? 0f : 1f);
    }

    public bool IsArrived()
    {
        if(navAgent.pathPending == false && navAgent.remainingDistance <= navAgent.stoppingDistance)
            return true;
        else
            return false;
    }

    public void StopImmediately() => navAgent.SetDestination(transform.position);
    public void MoveToTarget(Vector3 targetPos) => navAgent.SetDestination(targetPos);
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject != gameObject)
            return;

        if(navAgent == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, navAgent.destination);
    }
    #endif
}
