using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackCooldown = 0.1f;
    protected float latestAttackTime = 0f;
    protected abstract void Attack();


    protected virtual void Awake()
    {
         
    }

    public bool TryAttack()
    {
        if (AbleToAttack() == false)
            return false;

        Attack();
        latestAttackTime = Time.time;

        return true;
    }
    public bool AbleToAttack() => (Time.time - latestAttackTime > attackCooldown);
}
