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

    [Header("Sound")]
    [SerializeField] private AudioSource dashSoundSource;
	[SerializeField] private AudioClip[] dashClips;
    SoundManager soundManager;

	[HideInInspector] public Upgrade currentUpgrade;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		input = new InputSystem_Actions();
		Awake_Animation();
		soundManager = FindAnyObjectByType<SoundManager>();
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

		float currentMaxSpeed = maxSpeed;
        velocity = moveDir * maxSpeed;

        if (isDashing) {
            currentMaxSpeed = dashSpeed;
            if (moveDir.magnitude < 0.1) {
                velocity = transform.forward * dashSpeed;
            } else {
				velocity = moveDir * dashSpeed;
			}
            dashTime -= Time.fixedDeltaTime;
			if (dashTime <= 0) {
				isDashing = false;
			}
		}

        rb.linearVelocity = rb.linearVelocity + velocity;
		if (rb.linearVelocity.magnitude > currentMaxSpeed) {
			//rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
			rb.linearVelocity = Vector3.Lerp(rb.linearVelocity.normalized * currentMaxSpeed, rb.linearVelocity, 0.2f * Time.fixedDeltaTime);
		}

        if (currentUpgrade && currentUpgrade.isCollectible && input.Player.Interact.IsPressed()) {
            currentUpgrade.Collect();
			currentUpgrade = null;
        }
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

		soundManager.PlaySound(dashSoundSource, dashClips);
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