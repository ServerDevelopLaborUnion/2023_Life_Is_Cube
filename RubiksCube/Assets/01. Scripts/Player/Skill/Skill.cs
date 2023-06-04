using UnityEngine;

    
public abstract class Skill : MonoBehaviour
{
    public float specialAttackCooldown = 0.1f;
    public float latestSpecialAttackTime = 0f;
    protected abstract void SpecialAttack();


    protected virtual void Awake()
    {
        
    }

    public bool TrySpecialAttack()
    {
        if (AbleToSpecialAttack() == false)
            return false;

        SpecialAttack();
        latestSpecialAttackTime = Time.time;

        return true;
    }
    public bool AbleToSpecialAttack() => (Time.time - latestSpecialAttackTime > specialAttackCooldown);
}
