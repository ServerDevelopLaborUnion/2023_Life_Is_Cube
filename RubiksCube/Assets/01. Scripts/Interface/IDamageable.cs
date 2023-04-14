using UnityEngine;

public interface IDamageable
{
    public float MaxHP { get; }
    public float CurrentHP { get; }

    public void OnDamage(float damage, Vector3 point, Vector3 normal);
}
