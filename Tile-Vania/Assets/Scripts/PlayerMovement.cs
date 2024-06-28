using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	Vector2 moveInput;
	[SerializeField] float movementSpeed = 5f;
	[SerializeField] float jumpForce = 5f;
	[SerializeField] float climbingSpeed = 2f;
	float gravityScaleAtStart;
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	bool isDoubleJumped = false;

	Collider2D myCollider2D;

	// Start is called before the first frame update
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>(); // get rigidbody to change velocity  
		myAnimator = GetComponent<Animator>();
		myCollider2D = GetComponent<Collider2D>();
		gravityScaleAtStart = myRigidBody.gravityScale;
	}

	// Update is called once per frame
	void Update()
	{
		Run();
		FlipSprite();
		ClimbLadder();
		ClimbSprite();
	}



	private void Run()
	{
		// create new vector based on input, keep the y velocity the same 
		Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;
	}
	// flip the sprite
	private void FlipSprite()
	{
		// check if player has horizontal speed, means they are moving left or right
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		// if player has horizontal speed (moving left or right), flip the sprite
		if (playerHasHorizontalSpeed)
		{
			transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
			myAnimator.SetBool("isRunning", true);
		}
		else
		{
			myAnimator.SetBool("isRunning", false);
		}
	}
	void ClimbSprite()
	{
		// check if player has horizontal speed, means they are moving left or right
		bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
		// if player has horizontal speed (moving left or right), flip the sprite
		if (playerHasVerticalSpeed && myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
		{
			myAnimator.SetBool("isClimbing", true);
			myAnimator.SetBool("isRunning", false);
		}
		else
		{
			myAnimator.SetBool("isClimbing", false);
		}
	}
	void ClimbLadder()
	{
		// if player is touching the Ladder and has input to climb
		if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
		{
			// create new vector based on input, keep the y velocity the same 
			Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbingSpeed);
			myRigidBody.velocity = playerVelocity;
			myRigidBody.gravityScale = 0f;
		}
		else
		{
			myRigidBody.gravityScale = gravityScaleAtStart;
		}
	}
	// Input System listens for input here and we take it and assign it to moveInput 
	void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

	void OnJump(InputValue value)
	{
		// if player is touching the ground and jump button is pressed
		if (value.isPressed && myRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
			myRigidBody.velocity += jumpVelocityToAdd;
		}
		// if player is floating and second jump button is pressed
		if (value.isPressed && !isDoubleJumped && !myRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce / 2f);
			myRigidBody.velocity += jumpVelocityToAdd;
			isDoubleJumped = true;
		}
		// reset double jump if player touches ground
		if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			isDoubleJumped = false;
		}

	}

}
