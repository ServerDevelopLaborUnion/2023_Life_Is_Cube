using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackCooldown = 0.1f;

    protected float latestActiveTime = 0f;

    protected abstract void ActiveWeapon();

    public bool TryAticveWeapon()
    {
        if(Time.time - latestActiveTime < attackCooldown)
            return false;

        ActiveWeapon();
        latestActiveTime = Time.time;
        
        return true;
    }
}
