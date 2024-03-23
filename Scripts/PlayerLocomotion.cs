using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
	InputManager inputManager;

	Vector3 moveDirection;
	Transform cameraObject;
	Rigidbody rb;

	public float movementSpeed = 7;

	private void Awake()
	{
		inputManager = GetComponent<InputManager>();
		rb = GetComponent<Rigidbody>();
		cameraObject = Camera.main.transform;
	}
	
	public void HandleAllMovements()
	{
		HandleMovement();
		ClearMovement();
	}

	private void HandleMovement()
	{
		moveDirection = cameraObject.forward * inputManager.verticalInput;
		moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
		moveDirection.Normalize();
		moveDirection.y = 0;
		moveDirection *= movementSpeed;

		Vector3 movementVeloctiy = moveDirection;
		rb.velocity = movementVeloctiy;
	}

	private void ClearMovement()
	{
		// rb.velocity = Vector3.zero;
	}
}
