using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Animator animator;

    private Knockback knockback;


    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) { return; }
        rb.MovePosition(rb.position + moveDir.normalized * (moveSpeed * Time.deltaTime));
        animator.SetFloat("MoveX", moveDir.x);
        animator.SetFloat("MoveY", moveDir.y);
        animator.SetBool("IsMoving", true);

    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
