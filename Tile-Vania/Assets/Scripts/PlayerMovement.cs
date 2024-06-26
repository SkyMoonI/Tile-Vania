using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	Vector2 vector2;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnMove(InputValue value)
	{
		vector2 = value.Get<Vector2>();
		Debug.Log(vector2);
	}
}
