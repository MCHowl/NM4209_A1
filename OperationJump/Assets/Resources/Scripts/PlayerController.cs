using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool isJumping { get; set; }
	public Transform GroundCheck_Positive, GroundCheck_Negative;

	private Rigidbody2D rb2d;

	private float groundCheck_Radius = 0.01f;

	private Vector2 jumpForce = new Vector2(500, 500);
	private float speed = 2f;
	private int direction = 1;

	private Vector3 lastPosition, penultimatePosition;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		isJumping = true;
		lastPosition = Vector3.zero;
		penultimatePosition = Vector3.up;
	}

	void Update() {
		if (isGrounded()) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Flip();
				Jump();
			}
		}
	}

	private void FixedUpdate() {
		Move();

		// Code to prevent sticking to walls
		if (transform.position == lastPosition && lastPosition == penultimatePosition)
		{
			Flip();
		}
		else
		{
			penultimatePosition = lastPosition;
			lastPosition = transform.position;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Wall")) {
			Flip();
		}

		if (collision.gameObject.CompareTag("Respawn")) {
			Destroy(this.gameObject);
		}
	}

	void Move() {
		rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
	}

	public void Flip() {
		Vector3 flipScale = transform.localScale;
		flipScale.x *= -1;
		transform.localScale = flipScale;

		direction *= -1;
	}

	void Jump() {
		isJumping = true;
		rb2d.AddForce(jumpForce);
	}

	bool isGrounded() {
		// Check Centre
		Vector3 positiveCheckPosition = GroundCheck_Positive.position;
		Vector3 negativeCheckPosition = GroundCheck_Negative.position;

		Collider2D positiveCheck = Physics2D.OverlapCircle(positiveCheckPosition, groundCheck_Radius);
		Collider2D negativeCheck = Physics2D.OverlapCircle(negativeCheckPosition, groundCheck_Radius);

		if (positiveCheck != null) {
			if (positiveCheck.gameObject.CompareTag("Floor")) {
				if (negativeCheck == null) {
					return true;
				}
			}
		}

		//Check Left
		positiveCheckPosition.x -= 0.5f;
		negativeCheckPosition.x -= 0.5f;

		positiveCheck = Physics2D.OverlapCircle(positiveCheckPosition, groundCheck_Radius);
		negativeCheck = Physics2D.OverlapCircle(negativeCheckPosition, groundCheck_Radius);

		if (positiveCheck != null)
		{
			if (positiveCheck.gameObject.CompareTag("Floor"))
			{
				if (negativeCheck == null)
				{
					return true;
				}
			}
		}

		//Check Right
		positiveCheckPosition.x += 1;
		negativeCheckPosition.x += 1;

		positiveCheck = Physics2D.OverlapCircle(positiveCheckPosition, groundCheck_Radius);
		negativeCheck = Physics2D.OverlapCircle(negativeCheckPosition, groundCheck_Radius);

		if (positiveCheck != null)
		{
			if (positiveCheck.gameObject.CompareTag("Floor"))
			{
				if (negativeCheck == null)
				{
					return true;
				}
			}
		}

		return false;
	}
}
