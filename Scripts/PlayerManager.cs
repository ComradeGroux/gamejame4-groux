using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction moveAction;
	private Rigidbody rb;

	public float moveSpeed = 7;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();

		moveAction = playerInput.actions["Movement"];
	}

	private void OnEnable()
	{
		moveAction.performed += OnMovementPerformed;
		moveAction.canceled += OnMovementPerformed;
	}

	private void OnDisable()
	{
		moveAction.performed -= OnMovementPerformed;
		moveAction.canceled -= OnMovementPerformed;
	}

	private void OnMovementPerformed(InputAction.CallbackContext context)
	{
		Debug.Log("MOVEMENT");
		Vector2 move = context.ReadValue<Vector2>();
		Vector3 moveDir = new Vector3();

		moveDir.x = move.x;
		moveDir.y = 0;
		moveDir.z = move.y;

		rb.velocity = moveDir;
	}

	private void OnMovementCanceled(InputAction.CallbackContext context)
	{
		Debug.Log("CANCELED");
	}
}