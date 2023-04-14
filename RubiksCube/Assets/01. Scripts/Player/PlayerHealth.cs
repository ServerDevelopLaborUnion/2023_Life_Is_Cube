using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float MaxHP => playerStat.GetStat(StatFlags.MaxHP);
    public float CurrentHP => currentHP;
    
    private PlayerStat playerStat = null;
    private float currentHP = 0f;

    private void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    public void OnDamage(float damage, Vector3 point, Vector3 normal)
    {
        if(currentHP <= 0f)
            return;

        currentHP -= damage;

        if(currentHP <= 0f)
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
