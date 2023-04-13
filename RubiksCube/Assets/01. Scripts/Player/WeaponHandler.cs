using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon currentWeapon = null;

    public void ActiveWeapon()
    {
        currentWeapon.TryAticveWeapon();;
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
