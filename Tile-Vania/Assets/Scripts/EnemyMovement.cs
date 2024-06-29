using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;

	private Rigidbody2D myRigidBody;
	// Start is called before the first frame update
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		myRigidBody.velocity = new Vector2(moveSpeed, 0f);
	}

	// when box collider inside the wall, this method will be called
	void OnTriggerExit2D(Collider2D other)
	{
		moveSpeed = -moveSpeed;
		FlipEnemyFacing();
	}

	private void FlipEnemyFacing()
	{
		transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
	}
}
