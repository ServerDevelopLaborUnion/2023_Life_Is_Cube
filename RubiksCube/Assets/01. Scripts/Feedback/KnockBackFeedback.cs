using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockBackFeedback : Feedback
{
    private AIActionData aiActionData;
    private NavMovement movement;

    private void Awake()
    {
        aiActionData = transform.parent.GetComponent<AIActionData>();
        movement = transform.parent.GetComponent<NavMovement>();
    }

    public override void CreateFeedback()
    {
        Debug.Log(1);
        StartCoroutine(KnockBack());
    }

    public override void FinishFeedback()
    {
        movement.StopImmediately();
        StopAllCoroutines();
    }

    IEnumerator KnockBack()
    {
        Debug.Log(2);
        movement.StopImmediately();
        Debug.Log(transform.parent.name);
        transform.parent.position += (transform.parent.position - DEFINE.PlayerTrm.position).normalized;
        yield return null;
        Debug.Log(3);
        movement.StopImmediately();
    }
}
