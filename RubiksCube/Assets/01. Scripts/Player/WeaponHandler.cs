using System;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon = null;

    public bool TryActiveWeapon()
    {
        return currentWeapon.TryAticveWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
