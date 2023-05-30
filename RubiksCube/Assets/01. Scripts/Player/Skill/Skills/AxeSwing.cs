using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : Skill
{
    [Header("Attack")]
    [SerializeField] float detectRadius = 0.5f;
    [SerializeField] float damage = 10f;
    
    [Header("Effect")]
    [SerializeField] Transform effectPos = null;
    [SerializeField] EffectHandler specialEffectPrefab = null;

    //바꾸기
    public Animator animator = null;
    private Transform playerTrm = null;

    private readonly int onSpecialAttackHash = Animator.StringToHash("OnSpecialAttack");

    protected override void Awake()
    {
        base.Awake();
        // 나중에 추가 해야함
        //animator = /*transform.Find("Model").*/GetComponent<Animator>(); 
        playerTrm = transform.root;
    }

    protected override void SpecialAttack()
    {
        animator.SetBool(onSpecialAttackHash, true);
    }

    public void OnSpecialAnimationEvent()
    {
        SpecialAttackCastingDamage();
        SpecialAttackEffect();
    }

    public void OnSpecialAnimationEnd()
    {
        animator.SetBool(onSpecialAttackHash, false);
    }

    private void SpecialAttackCastingDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(playerTrm.position, detectRadius * 3f, DEFINE.EnemyLayer);
        foreach (Collider col in enemies)
            if (col.TryGetComponent<IDamageable>(out IDamageable id))
                id?.OnDamage(damage * 3f, col.transform.position, col.transform.position);
    }

    private void SpecialAttackEffect()
    {
        EffectHandler effect = PoolManager.Instance.Pop(specialEffectPrefab) as EffectHandler;
        effect.transform.position = playerTrm.position;
        effect.transform.rotation = playerTrm.rotation;

        effect?.PlayEffects();
    }
}
