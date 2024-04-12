using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Material[] intMat;
    public Material[] extMat;

    private bool isDashing = false;
    public float _dashSpeed = 50.0f;
    public float _dashTime = 0.05f;

    public float attackRange = 2;
    public float throwBack = 200.0f;
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

        SkinnedMeshRenderer mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mat = new Material[2];

        Debug.Log(playerInput.playerIndex);
        mat[1] = intMat[playerInput.playerIndex];
        mat[0] = extMat[playerInput.playerIndex];
        Debug.Log(mat[0].ToString());
        mesh.materials = mat;
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

	public void Update()
	{
		Debug.DrawRay(transform.position, transform.forward);
	}

	private void OnMovementPerformed(InputAction.CallbackContext context)
	{
		GetComponent<Animator>().SetBool("isMoving", true);
		
		Vector2 move = context.ReadValue<Vector2>();
		Vector3 moveDir = new Vector3();

		moveDir.x = move.x;
		moveDir.y = 0;
		moveDir.z = move.y;

		moveDir.Normalize();
		moveDir *= moveSpeed;

		rb.velocity = moveDir;
		if (rb.velocity != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10000);
		}

		transform.forward = rb.velocity.normalized;
	}
	private void OnMovementCanceled(InputAction.CallbackContext context)
	{
		GetComponent<Animator>().SetBool("isMoving", false);
		rb.velocity = Vector3.zero;
	}

	private void OnGrabPerformed(InputAction.CallbackContext context)
	{
		GetComponent<GrabMechanic>().Grab();
	}
	private void OnGrabCanceled(InputAction.CallbackContext context)
	{
	}

	private void OnDashPerformed(InputAction.CallbackContext context)
	{
		if (!isDashing)
			StartCoroutine(DashCoroutine());
	}
	private IEnumerator DashCoroutine()
	{
		isDashing = true;
		float startTime = Time.time;
		while (Time.time < startTime + _dashTime)
		{
			transform.Translate(Vector3.forward * _dashSpeed * Time.deltaTime);
			yield return null;
		}
		isDashing = false;
	}
	private void OnDashCanceled(InputAction.CallbackContext context)
	{
	}

	private void OnAttackPerformed(InputAction.CallbackContext context)
	{
		Vector3 attackPos = transform.position + transform.forward * attackRange;
		Collider[] hitColliders = Physics.OverlapSphere(attackPos, attackRange);

		foreach (Collider collider in hitColliders)
		{
			if (collider.CompareTag("Goober") && collider.gameObject != gameObject)
			{
				if (collider.transform.parent != gameObject)
				{
					Debug.Log("NOT THE SAME");
				}
				else
					Debug.Log("THERE ARE THE SAME");
			}
			else if (collider.gameObject == gameObject)
				Debug.Log("SAME gameObject");
		}
	}
	private void PlayerHit(GameObject hitPlayer)
	{
		Rigidbody parent = hitPlayer.GetComponentInParent<Rigidbody>();
		Vector3 direction = (hitPlayer.transform.position - transform.forward).normalized;
		parent.AddForce(direction * throwBack, ForceMode.Impulse);
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