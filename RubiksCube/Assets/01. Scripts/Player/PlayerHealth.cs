using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float MaxHP => playerStat.GetStat(StatFlags.MaxHP);
    public float CurrentHP => currentHP;

    private PlayerStat playerStat = null;
    private float currentHP = 0f;

    private HPPanel hpPanel = null;
    private StatPanel statPanel = null;

    private void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
        hpPanel = DEFINE.MainCanvas.Find("HPPanel")?.GetComponent<HPPanel>();
        statPanel = DEFINE.MainCanvas.Find("StatPanel")?.GetComponent<StatPanel>();

        //Debug.Log($"MaxHP : {MaxHP} \n(PlayerHealth Awake)");
    }

    private void Start()
    {
        currentHP = MaxHP;
        statPanel?.DisplayHP(currentHP, MaxHP);
    }

    public void OnDamage(float damage, Vector3 point, Vector3 normal)
    {
        if (currentHP <= 0f)
            return;

        currentHP -= damage;
        hpPanel?.SetHP(currentHP / MaxHP);
        statPanel?.DisplayHP(currentHP, MaxHP);

        if (currentHP <= 0f)
            OnDie();
    }

    private void OnDie()
    {
    }

    public void Init()
    {
        currentHP = MaxHP;
    }
}
