using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public partial class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	public float maxSpeed = 6f;
	public float acceleration = 20f;
	public float deceleration = 25f;

	[Header("Rotation")]
	public float rotationSpeed = 12f;

	[Header("Dash")]
	public float dashSpeed = 18f;
	public float dashDuration = 0.15f;
	public float dashCooldown = 1f;

	private Rigidbody rb;
	private InputSystem_Actions input;

	private Vector2 moveInput;
	private Vector3 velocity;

    private Vector3 moveDir;
    private Vector3 lookDir;

	private bool IsLookingCardinal;

    private bool isDashing;
	private float dashTime;
	private float lastDashTime;

    [Header("Camera")]
	[SerializeField] private CameraController cameraController;

	[SerializeField] private Transform lookNorth;
	[SerializeField] private Transform lookSouth;
	[SerializeField] private Transform lookEast;
	[SerializeField] private Transform lookWest;
	private Transform lastCameraTarget;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		input = new InputSystem_Actions();
		Awake_Animation();
	}

	private void OnEnable()
	{
		input.Enable();
		OnEnable_NinjaSigns();
	}

	private void OnDisable()
	{
		OnDisable_NinjaSigns();
		input.Disable();
	}

	private void Update()
	{
		moveInput = input.Player.Move.ReadValue<Vector2>();
		HandleCameraInput();
		if (input.Player.Sprint.WasPressedThisFrame())
		{
			TryDash();
		}

		Update_NinjaSigns();
	}

	private void FixedUpdate()
	{
        moveDir = new Vector3(moveInput.x, 0, moveInput.y);

		if (!IsLookingCardinal)
		{
            lookDir = moveDir;
        }

        // Rotate player to face movement direction
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        if (moveDir.magnitude > 1)
            moveDir.Normalize();

		if (!isDashing)
		{
			Vector3 targetVelocity = moveDir * maxSpeed;

			float accel = (moveDir.magnitude > 0.1f)
				? acceleration
				: deceleration;

			velocity = Vector3.MoveTowards(
				velocity,
				targetVelocity,
				accel * Time.fixedDeltaTime
			);
		}
		else
		{
			dashTime -= Time.fixedDeltaTime;

			if (dashTime <= 0)
				isDashing = false;
		}

		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}

	private void TryDash()
	{
		if (Time.time < lastDashTime + dashCooldown)
			return;

		Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y);

		if (dir.sqrMagnitude < 0.01f)
			dir = transform.forward;

		dir.Normalize();

		velocity = dir * dashSpeed;

		isDashing = true;
		dashTime = dashDuration;
		lastDashTime = Time.time;
	}

	private void HandleCameraInput()
	{
        if (input.Player.LookNorth.IsPressed())
        {
			PlayerLookCardinal(lookNorth);
        }
		else if (input.Player.LookSouth.IsPressed())
		{
            PlayerLookCardinal(lookSouth);

        }
        else if (input.Player.LookEast.IsPressed())
		{
            PlayerLookCardinal(lookEast);

        }
        else if (input.Player.LookWest.IsPressed())
        {
            PlayerLookCardinal(lookWest);
        }
        else
		{
            cameraController.ResetToPlayer();
            IsLookingCardinal = false;
            input.Player.Move.Enable();
            input.Player.Sprint.Enable();
        }
    }

    private void PlayerLookCardinal(Transform look)
    {
        cameraController.SetCameraTarget(look);
        lookDir = look.forward;

        IsLookingCardinal = true;
        input.Player.Move.Disable();
        input.Player.Sprint.Disable();
    }

    private void OnCollisionEnter(Collision collision) 
	{
		HandleCollectableCollisionEnter(collision);
	}

    private void OnCollisionStay(Collision collision) 
	{
		HandleCollectableCollisionStay(collision);
	}

	private void OnCollisionExit(Collision collision) 
	{
		HandleCollectableCollisionExit(collision);
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Tent"))
		{
			HandleCollectableTentDrop();
        }
    }

    private void EnablePlayerMovement() 
	{
        input.Player.Move.Enable();
		input.Player.Sprint.Enable();
        input.Player.LookNorth.Enable();
        input.Player.LookNorth.Enable();
        input.Player.LookNorth.Enable();
        input.Player.LookNorth.Enable();
	}

	private void DisablePlayerMovement()
	{
        input.Player.Move.Disable();
		input.Player.Sprint.Disable();
        input.Player.LookNorth.Disable();
        input.Player.LookNorth.Disable();
        input.Player.LookNorth.Disable();
        input.Player.LookNorth.Disable();
	}
}