using System.Collections.Generic;
using UnityEngine;

public class AIBrain : PoolableMono
{
    [SerializeField] AIState currentState;

    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private GameObject focusBorder;

    private EnemyHealth health = null;
    public EnemyHealth Health => health;

    private NavMovement navMovement;

    private bool isFocused = false;
    public bool IsFocused {
        set {
            isFocused = value;
            focusBorder?.SetActive(isFocused);
        }
    }

    public bool IsDead { get; set; }

    private void Awake()
    {
        List<AIState> states = new List<AIState>();
        transform.Find("AI").GetComponentsInChildren<AIState>(states);

        states.ForEach(state => state.SetUp(transform));

        focusBorder = transform.Find("FocusBorder").gameObject;

        health = GetComponent<EnemyHealth>();
        health?.Init();

        navMovement = GetComponent<NavMovement>();
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
        IsDead = false;
    }

    public void Init()
    {
        navMovement.NavAgent.enabled = true;
    }

    public void Release()
    {
        StageManager.Instance.RemoveEnemy(this);
        PoolManager.Instance.Push(this);
    }
}
