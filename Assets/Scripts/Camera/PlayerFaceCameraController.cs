using UnityEngine;

public class PlayerFaceCameraController : MonoBehaviour
{
	[Header("Target")]
	[SerializeField] private Transform player;

	[Header("Camera Position")]
	[SerializeField] private Vector3 offset = new Vector3(0, 1.7f, -3f);

	[Header("Camera Settings")]
	[SerializeField] private float followSpeed = 8f;
	[SerializeField] private float rotationSpeed = 8f;


	private void LateUpdate()
	{
		if (player == null)
			return;

		FollowPlayer();
		LookAtPlayer();
	}


	private void FollowPlayer()
	{
		Vector3 desiredPosition = player.position +
								  player.rotation * offset;

		transform.position = Vector3.Lerp(
			transform.position,
			desiredPosition,
			followSpeed * Time.deltaTime
		);
	}


	private void LookAtPlayer()
	{
		Vector3 lookTarget = player.position + Vector3.up * 1.5f;

		Quaternion targetRotation = Quaternion.LookRotation(
			lookTarget - transform.position
		);

		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			targetRotation,
			rotationSpeed * Time.deltaTime
		);
	}
}
