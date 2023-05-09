using System.Collections.Generic;
using UnityEngine;

public class AIBrain : PoolableMono
{
    [SerializeField] AIState currentState;

    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private EnemyHealth health = null;

    private void Awake()
    {
        List<AIState> states = new List<AIState>();
        transform.Find("AI").GetComponentsInChildren<AIState>(states);

        states.ForEach(state => state.SetUp(transform));

        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        targetTrm = DEFINE.PlayerTrm;
    }

    private void Update()
    {
        currentState?.StateUpdate();
    }

    public void ChangeState(AIState targetState)
    {
        currentState?.OnStateExit();
        currentState = targetState;
        currentState?.OnStateEnter();
    }

    public override void Reset()
    {
        health?.Init();
    }
}
