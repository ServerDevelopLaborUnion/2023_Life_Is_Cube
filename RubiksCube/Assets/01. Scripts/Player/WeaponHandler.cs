using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("!직업 선택창 순서랑 맞추기!")]
    [SerializeField] List<Weapon> skills = new List<Weapon>();

    [SerializeField] Weapon currentWeapon = null;

    public bool TryActiveWeapon()
    {
        return currentWeapon.TryAttack();
    }

    public void SetWeapon(int idx)
    {
        currentWeapon = skills[idx];
    }
}
