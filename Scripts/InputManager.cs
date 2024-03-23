using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	PlayerControls playerControls;

	public Vector2 movementInput;
	public float verticalInput;
	public float horizontalInput;

	private void OnEnable()
	{
		if (playerControls == null)
		{
			playerControls = new PlayerControls();

			playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
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
		if (movementInput.y > 0.2 || movementInput.y < -0.2)
			verticalInput = movementInput.y;
		else
			verticalInput = 0;

		if (movementInput.x > 0.2 || movementInput.x < -0.2)
			horizontalInput = movementInput.x;
		else
			horizontalInput = 0;
	}
}
