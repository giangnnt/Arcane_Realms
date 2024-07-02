using UnityEngine;

public class Spear : MonoBehaviour, IWeapon
{

    [SerializeField]
    private GameObject slashAnimPrefab;
    [SerializeField]
    private Transform slashAnimSpawnPoint;
    [SerializeField]
    private WeaponInfo weaponInfo;

    private Animator animator;
    private GameObject slashAnim;
    private bool isAttackingState;
    private Transform weaponCollider;


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
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;

        weaponCollider.gameObject.SetActive(false);
    }


    private void Update()
    {

        animator.SetBool("IsAttacking", isAttackingState);
        MouseFollowWithOffset();
    }

    public void HideWeapon()
    {
        isAttackingState = false;
        animator.ResetTrigger("Attack");
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        isAttackingState = true;
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
    //private void MouseFollowWithOffset()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    Vector3 playerPos = PlayerController.Instance.transform.position;
    //    Vector3 direction = mousePos - playerPos;

    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    //    weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}
}
