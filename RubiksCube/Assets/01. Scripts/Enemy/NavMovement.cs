using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{
    private NavMeshAgent navAgent = null;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
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
}
