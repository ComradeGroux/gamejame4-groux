using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	PlayerControls playerControls;

	public Vector2 movementInput;
	public float verticalInput;
	public float horizontalInput;
	float deadzone = 0.1f;

	private void OnEnable()
	{
		if (playerControls == null)
		{
			playerControls = new PlayerControls();

			playerControls.PlayerMovement.Movement.performed += i => { movementInput = i.ReadValue<Vector2>(); Debug.Log("running"); };
			playerControls.PlayerMovement.Movement.canceled += j => { movementInput = Vector2.zero; Debug.Log("canceled"); };
		}

		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

	public void HandleAllInputs()
	{
		HandleMovementInput();
	}

	private void HandleMovementInput()
	{
		verticalInput = movementInput.y;
		horizontalInput = movementInput.x;
	}
}
