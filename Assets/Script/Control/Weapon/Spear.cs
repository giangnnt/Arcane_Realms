using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour
{

    [SerializeField]
    private GameObject slashAnimPrefab;
    [SerializeField]
    private Transform slashAnimSpawnPoint;
    [SerializeField]
    private Transform weaponCollider;
    [SerializeField]
    private float swordAttackCD = .2f;

    private PlayerControls playerControls;
    private Animator animator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnim;
    private bool attackButtonDown = false;

    private bool isAttacking = false; // Track attack state
    private bool isAnimAttacking = false;
    private float lastAttackTime = 0f; // Time of the last attack


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        weaponCollider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }
    private void Update()
    {

        animator.SetBool("IsAttacking", isAttacking);
        if (isAttacking && Time.time > lastAttackTime + 2f)
        {
            animator.ResetTrigger("Attack");
            isAttacking = false;
        }
        MouseFollowWithOffset();
        Attack();
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAnimAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            isAnimAttacking = true;
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            lastAttackTime = Time.time; // Update last attack time
            StartCoroutine(AttackCDRoutine());
        }
    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAnimAttacking = false;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
