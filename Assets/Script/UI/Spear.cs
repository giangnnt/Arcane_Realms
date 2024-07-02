using UnityEngine;

public class Spear : MonoBehaviour, IWeapon
{

    [SerializeField]
    private GameObject slashAnimPrefab;
    [SerializeField]
    private WeaponInfo weaponInfo;
    [SerializeField]
    private Transform weaponCollider;

    private Animator animator;
    private GameObject slashAnim;
    private bool isAttackingState;
    private bool secondSwing = true;

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider.gameObject.SetActive(false);
    }


    private void Update()
    {
        animator.SetBool("IsAttacking", isAttackingState);
    }

    public void HideWeapon()
    {
        isAttackingState = false;
        animator.ResetTrigger("Attack");
        secondSwing = true;
    }
    public void Attack()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        animator.SetTrigger("Attack");
        isAttackingState = true;
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, transform.parent.position, transform.parent.rotation);
        slashAnim.GetComponent<SpriteRenderer>().flipY = secondSwing;
        secondSwing = !secondSwing;
        Vector3 direction = worldMousePos - transform.parent.position;

        direction.Normalize();

        slashAnim.transform.position += direction * 5;

    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    //public void SwingDownFlipAnimEvent()
    //{
    //    slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

    //}

    //public void SwingUpFlipAnimEvent()
    //{
    //    slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}
}
