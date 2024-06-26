using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	Vector2 moveInput;
	[SerializeField] float movementSpeed = 5f;
	Rigidbody2D myRigidBody;

	// Start is called before the first frame update
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>(); // get rigidbody to change velocity  
	}

	// Update is called once per frame
	void Update()
	{
		Run();
	}
	private void Run()
	{
		// create new vector based on input, keep the y velocity the same 
		Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;
	}
	// Input System listens for input here and we take it and assign it to moveInput 
	void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
		Debug.Log(moveInput);
	}
}
