using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPuzzleController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("IsMoving", success);
            //Debug.Log(moveSpeed * Time.fixedDeltaTime * movementInput);     
        }
        else
        {
            animator.SetBool("IsMoving", false);

        }
    }
    private void FixedUpdate()
    {

        //Debug.Log("DirectionInput: " + movementInput);

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
        Debug.Log("Dir: " + direction);
        Debug.Log("Count: " + count);
        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movementInput);
            return true;

        }
        else
        {
            return false;


        }
    }

    void OnMove(InputValue movementValue)
    {

        movementInput = movementValue.Get<Vector2>();
        Debug.Log("OnMove: " + movementValue.Get<Vector2>());
        if (movementInput != Vector2.zero)
        {

            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
        }
    }
}
