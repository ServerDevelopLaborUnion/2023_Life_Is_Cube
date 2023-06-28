using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent = null;
    public UnityEvent OnDeadEvent = null;


    [SerializeField] float maxHP = 40f;
    private float currentHP = 0f;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private AIBrain aiBrain = null;
    private AIActionData aiActionData = null;

    private void Awake()
    {
        aiBrain = GetComponent<AIBrain>();
        aiActionData = GetComponent<AIActionData>();
    }

    public void OnDamage(float damage, Vector3 point, Vector3 normal)
    {
        if(currentHP <= 0f)
            return;

        aiActionData.HitNormal = normal;
        aiActionData.HitPoint = point;

        currentHP -= damage;

        OnHitEvent?.Invoke();
        
        if(currentHP <= 0f)
            OnDeadEvent?.Invoke();
    }

    public void Init()
    {
        currentHP = maxHP;
    }
}
