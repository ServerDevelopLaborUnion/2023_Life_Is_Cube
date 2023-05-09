using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon = null;

    public bool TryActiveWeapon()
    {
        return currentWeapon.TryAttack();
    }

    public bool TryActiveSpecialAttack()
    {
        return currentWeapon.TrySpecialAttack();
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
