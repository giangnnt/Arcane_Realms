using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponInfo weaponInfo;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform arrowSpawnpoint;


    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnpoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
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
