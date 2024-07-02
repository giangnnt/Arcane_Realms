using UnityEngine;

public class Axe : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponInfo weaponInfo;

    public void Attack()
    {
        Debug.Log("Axe Attack");
    }

    public void HideWeapon()
    {
        Debug.Log("HdingWeapon");
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
