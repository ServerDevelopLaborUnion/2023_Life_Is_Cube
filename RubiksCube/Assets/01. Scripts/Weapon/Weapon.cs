using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackCooldown = 0.1f;
    [SerializeField] protected float specialAttackCooldown = 0.1f;

    protected float latestAttackTime = 0f;
    protected float latestSpecialAttackTime = 0f;

    protected abstract void Attack();
    protected abstract void SpecialAttack();

    private Image skillImage = null;

    protected virtual void Awake()
    {
        skillImage = UIManager.Instance.InputPanel.Find("InteractButton/SpecialButton/FillImage").GetComponent<Image>();
        Debug.Log(Time.time - latestSpecialAttackTime / specialAttackCooldown);
    }

    public bool TryAttack()
    {
        if(AbleToAttack() == false)
            return false;

        Attack();
        latestAttackTime = Time.time;

        return true;
    }

    public bool TrySpecialAttack()
    {
        if(AbleToSpecialAttack() == false)
            return false;

        SpecialAttack();
        latestSpecialAttackTime = Time.time;

        return true;
    }

    private void Update()
    {
        if(skillImage != null && AbleToSpecialAttack() == false)
            skillImage.fillAmount = ((Time.time - latestSpecialAttackTime) / specialAttackCooldown);
    }


    public bool AbleToAttack() => (Time.time - latestAttackTime > attackCooldown);
    public bool AbleToSpecialAttack() => (Time.time - latestSpecialAttackTime > specialAttackCooldown);
}
