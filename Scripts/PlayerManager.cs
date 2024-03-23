using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction moveAction;
	private InputAction grabAction;
	private InputAction dashAction;
	private InputAction attackAction;
	private InputAction tauntAction;

	private Rigidbody rb;

	public float moveSpeed = 7;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();

		moveAction = playerInput.actions["Movement"];
		grabAction = playerInput.actions["Grab"];
		dashAction = playerInput.actions["Dash"];
		attackAction = playerInput.actions["Attack"];
		tauntAction = playerInput.actions["Taunt"];
}

	private void OnEnable()
	{
		moveAction.performed += OnMovementPerformed;
		moveAction.canceled += OnMovementCanceled;
		grabAction.performed += OnGrabPerformed;
		grabAction.canceled += OnGrabCanceled;
		dashAction.performed += OnDashPerformed;
		dashAction.canceled += OnDashCanceled;
		attackAction.performed += OnAttackPerformed;
		attackAction.canceled += OnAttackCanceled;
		tauntAction.performed += OnTauntPerformed;
		tauntAction.canceled += OnTauntCanceled;
	}

	private void OnDisable()
	{
		moveAction.performed -= OnMovementPerformed;
		moveAction.canceled -= OnMovementCanceled;
		grabAction.performed -= OnGrabPerformed;
		grabAction.canceled -= OnGrabCanceled;
		dashAction.performed -= OnDashPerformed;
		dashAction.canceled -= OnDashCanceled;
		attackAction.performed -= OnAttackPerformed;
		attackAction.canceled -= OnAttackCanceled;
		tauntAction.performed -= OnTauntPerformed;
		tauntAction.canceled -= OnTauntCanceled;
	}

	private void OnMovementPerformed(InputAction.CallbackContext context)
	{
		Vector2 move = context.ReadValue<Vector2>();
		Vector3 moveDir = new Vector3();

		moveDir.x = move.x;
		moveDir.y = 0;
		moveDir.z = move.y;

		moveDir.Normalize();
		moveDir *= moveSpeed;

		rb.velocity = moveDir;
	}
	private void OnMovementCanceled(InputAction.CallbackContext context)
	{
		rb.velocity = Vector3.zero;
	}

	private void OnGrabPerformed(InputAction.CallbackContext context)
	{
	}
	private void OnGrabCanceled(InputAction.CallbackContext context)
	{
	}

	private void OnDashPerformed(InputAction.CallbackContext context)
	{
	}
	private void OnDashCanceled(InputAction.CallbackContext context)
	{
	}

	private void OnAttackPerformed(InputAction.CallbackContext context)
	{
	}
	private void OnAttackCanceled(InputAction.CallbackContext context)
	{
	}

	private void OnTauntPerformed(InputAction.CallbackContext context)
	{
	}
	private void OnTauntCanceled(InputAction.CallbackContext context)
	{
	}
}