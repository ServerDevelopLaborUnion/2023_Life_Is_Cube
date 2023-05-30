using UnityEngine;
using static DEFINE;

public class Knife : Weapon
{
    [Header("Attack")]
    [SerializeField] Transform damageCaster = null;
    [SerializeField] float detectRadius = 0.5f;
    [SerializeField] float damage = 10f;

    [Header("Effect")]
    [SerializeField] Transform effectPos = null;
    [SerializeField] EffectHandler effectPrefab = null;

    private Animator animator = null;
    private Transform playerTrm = null;

    private readonly int onAttackHash = Animator.StringToHash("OnAttack");

    protected override void Awake()
    {
        base.Awake();
        animator = /*transform.Find("Model").*/GetComponent<Animator>();
        playerTrm = transform.root;
    }

    protected override void Attack()
    {
        //검 휘두르기 시작
        animator.SetBool(onAttackHash, true);
    }

    public void OnAnimationEvent()
    {
        //실질적 공격이 이루어졌을 때
        CastingDamage();
        Effect();
    }

    public void OnAnimationEnd()
    {
        //모든 공격이 끝났을 때
        animator.SetBool(onAttackHash, false);
    }

    //이거 해야됨
    private void CastingDamage()
    {
        RaycastHit[] hits = Physics.SphereCastAll(damageCaster.position - (damageCaster.forward * detectRadius * 2), detectRadius, damageCaster.forward, detectRadius * 2, EnemyLayer);
        foreach (RaycastHit hit in hits)
            if (hit.point != Vector3.zero)
                if (hit.collider.TryGetComponent<IDamageable>(out IDamageable id))
                    id?.OnDamage(damage, hit.point, hit.normal);
    }

    private void Effect()
    {
        EffectHandler effect = PoolManager.Instance.Pop(effectPrefab) as EffectHandler;
        effect.transform.position = effectPos.position;
        effect.transform.rotation = effectPos.rotation;

        effect?.PlayEffects();
    }

#if UNITY_EDITOR
    // 얘는 안나누고 냅뒀음
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(damageCaster.position, detectRadius);

        if (playerTrm == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerTrm.position, detectRadius * 3f);
    }

#endif
}
