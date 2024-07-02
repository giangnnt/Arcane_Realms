using UnityEngine;

public class Lance : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponInfo weaponInfo;
    // Start is called before the first frame update
    public void Attack()
    {
        Debug.Log("Lance Attack");
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
