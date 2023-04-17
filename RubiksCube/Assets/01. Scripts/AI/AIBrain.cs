using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] AIState currentState;

    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private NavMovement navMovement = null;
    public NavMovement NavMovement => navMovement;

    private AnimatorHandler enemyAnimator = null;
    public AnimatorHandler EnemyAnimator => enemyAnimator;

    private void Awake()
    {
        navMovement = GetComponent<NavMovement>();
        enemyAnimator = transform.Find("Model").GetComponent<AnimatorHandler>();
        
        List<AIState> states = new List<AIState>();
        transform.Find("AI").GetComponentsInChildren<AIState>(states);

        states.ForEach(state => state.SetUp(transform.root));
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
}
