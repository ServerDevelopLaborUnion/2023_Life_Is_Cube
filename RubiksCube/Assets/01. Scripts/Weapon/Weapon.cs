using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackCooldown = 0.1f;
    [SerializeField] protected float specialAttackCooldown = 0.1f;

    protected float latestAttackTime = 0f;
    protected float latestSpecialAttackTime = 0f;

    protected abstract void Attack();
    protected abstract void SpecialAttack();

    public bool TryAttack()
    {
        if(AbleToAttack())
            return false;

        Attack();
        latestAttackTime = Time.time;
        
        return true;
    }

    public bool TrySpecialAttack()
    {
        if(AbleToSpecialAttack())
            return false;

        SpecialAttack();
        latestSpecialAttackTime = Time.time;

        return true;
    }


    public bool AbleToAttack() => (Time.time - latestAttackTime < attackCooldown);
    public bool AbleToSpecialAttack() => (Time.time - latestSpecialAttackTime < specialAttackCooldown);
}
