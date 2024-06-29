using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    [SerializeField]
    private float moveSpeed = 1.0f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private bool facingLeft = false;
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        facingLeft = mousePos.x < playerScreenPoint.x;
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
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
