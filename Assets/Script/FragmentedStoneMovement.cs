using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentedStoneMovement : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the stone's movement default velocity 2 unit/s
    public float bounceFounce = 5.0f; // Force applied when stones collide
	public bool isMovingRight = true; // Initial direction

	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f; // Disable gravity for the stone
		rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; // Prevent vertical movement and rotation
		rb.gravityScale = 0; // Ensure the stone is not affected by gravity
		rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; // Prevent vertical movement and rotation

		// Set initial velocity to move the stone along the x-axis
		rb.velocity = new Vector2(isMovingRight ? speed : -speed, rb.velocity.y);
	}

    // Update is called once per frame
    void Update()
    {
		//Move the stone along the x-axis with velocity of unit/s
		rb.velocity = new Vector2(isMovingRight ? speed : -speed, rb.velocity.y);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Handle collision with other objects
        if(collision.gameObject.CompareTag("FragementedStone"))
        {
			Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
			forceDirection.y = 0; // Ensure force is applied only along the x-axis

			rb.AddForce(forceDirection * bounceFounce, ForceMode2D.Impulse);
			collision.rigidbody.AddForce(-forceDirection * bounceFounce, ForceMode2D.Impulse);

			// Reverse direction
			isMovingRight = !isMovingRight;
		}
        if (collision.gameObject.CompareTag("Fence"))
        {
			isMovingRight = !isMovingRight; // Reverse direction on wall collision
		}
	}
}
