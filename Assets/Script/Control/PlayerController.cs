using Assets.Script;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public bool FacingLeft { get { return facingLeft; } }
    public bool FacingUp { get { return facingLeft; } }

    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float dashSpeed = 2.0f;
    [SerializeField]
    private TrailRenderer myTrailRenderer;
    [SerializeField]
    private Transform weaponCollider;


    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float startingMoveSpeed;
    private Knockback knockback;
    private CharacterMoveBox characterMoveBox;

    private bool facingLeft = false;
    private bool facingUp = false;
    private bool isDashing = false;

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        playerControls.PuzzleMapAction.PushMovement.performed += _ => PushMovement();
        startingMoveSpeed = moveSpeed;

        ActiveInventory.Instance.EquipStartingWeapon();
    }

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        facingLeft = mousePos.x < playerScreenPoint.x;
        facingUp = mousePos.y > playerScreenPoint.y;

        if (movement != Vector2.zero)
        {
            myAnimator.SetBool("IsMoving", true);
        }
        else
        {
            myAnimator.SetBool("IsMoving", false);
        }
        myAnimator.SetFloat("XInput", mousePos.x - playerScreenPoint.x);
        myAnimator.SetFloat("YInput", mousePos.y - playerScreenPoint.y);
    }
    private void Move()
    {
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.IsDead) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash()
    {
        if (!isDashing & Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private void PushMovement()
    {
        Vector2 pushDirection = movement.normalized;
        CharacterMoveBox.Instance.TryPushBox(pushDirection);
    }
}
