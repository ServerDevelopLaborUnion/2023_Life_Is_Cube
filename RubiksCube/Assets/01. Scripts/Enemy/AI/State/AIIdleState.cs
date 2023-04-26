using System.Collections;
using UnityEngine;

public class AIIdleState : AIState
{
    [SerializeField] float patrolRadius = 5f;
    [SerializeField] float stoppingDuration = 10f;
    private Vector3 patrolPos;
    private bool stopping = false;

    public override void OnStateEnter()
    {
    }

    public override void StateUpdate()
    {
        if(navMovement.IsArrived() && stopping == false)
        {
            if(Random.Range(0, 3f) > 1f)
            {
                patrolPos = GetRandCircumion();
                navMovement.MoveToTarget(patrolPos);
            }
            else
            {
                stopping = true;

                StartCoroutine(DelayCoroutine(Random.Range(0f, stoppingDuration), () => {
                    stopping = false;
                }));
            }

        }

        base.StateUpdate();
    }

    public override void OnStateExit()
    {
        navMovement.StopImmediately();
    }

    private Vector3 GetRandCircumion()
    {
        float randAngle = Random.Range(0, 360f);
        return new Vector3(Mathf.Cos(randAngle), 0, Mathf.Sin(randAngle)) * patrolRadius + transform.position;
    }

    private IEnumerator DelayCoroutine(float delay, System.Action callback = null)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject != gameObject)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, patrolPos);
    }
    #endif
}
