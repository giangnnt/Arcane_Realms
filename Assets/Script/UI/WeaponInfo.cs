using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    // Start is called before the first frame update
    public GameObject weaponPrefab;
    public float weaponCoolDown;
    public int weaponDamage;
    public float weaponRange;
}
