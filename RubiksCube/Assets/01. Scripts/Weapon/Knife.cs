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

    private readonly int onSlashHash = Animator.StringToHash("OnSlash");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTrm = transform.root;
    }

    protected override void ActiveWeapon()
    {
        //검 휘두르기 시작
        animator.SetBool(onSlashHash, true);
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
        animator.SetBool(onSlashHash, false);
    }

    private void CastingDamage()
    {
        //대미지 받아랏
        Collider[] detectedColliders = Physics.OverlapSphere(damageCaster.position, detectRadius, EnemyLayer);

        if(detectedColliders.Length <= 0)
            return;

        IDamageable id = null;
        foreach(Collider col in detectedColliders)
        {
            if(col.TryGetComponent<IDamageable>(out id))
            {
                Vector3 normal = -(col.transform.position - playerTrm.position);
                normal.y = 0;
                id?.OnDamage(damage, Vector3.zero, normal);
            }
        }
    }

    private void Effect()
    {
        EffectHandler effect = PoolManager.Instance.Pop(effectPrefab.name) as EffectHandler;
        effect.transform.position = effectPos.position;
        effect.transform.rotation = effectPos.rotation;

        effect?.PlayEffects();
    }

    #if UNITY_EDITOR
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(damageCaster.position, detectRadius);
    }
    
    #endif
}
