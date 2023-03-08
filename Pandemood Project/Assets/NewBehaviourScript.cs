using UnityEngine;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour
{
	public Transform target = null;

	public float distanceFromTarget = 10.0f;

	public float elevationFromTarget = 5.0f;

	public KeyCode forwardMovementInput = KeyCode.W;

	public KeyCode backwardMovementInput = KeyCode.S;

	public KeyCode raiseElevationInput = KeyCode.Space;

	public KeyCode lowerElevationInput = KeyCode.LeftControl;

	public float movementSpeed = 5.0f;

	public KeyCode leftRotationInput = KeyCode.A;

	public KeyCode rightRotationInput = KeyCode.D;

	public float rotationSpeed = 90.0f;

	private void LateUpdate()
	{
		if (!target)
		{
			if (Application.isPlaying)
			{
				Debug.LogWarning("Target is missing!", this);
				enabled = false;
			}

			return;
		}

		// Forward/backward movement.
		bool moveForward = Input.GetKey(forwardMovementInput);
		bool moveBackward = Input.GetKey(backwardMovementInput);
		if (moveForward != moveBackward)
		{
			distanceFromTarget += (moveBackward ? movementSpeed : -movementSpeed) * Time.deltaTime;
		}

		// Raise/Lower movement.
		bool lowerElevation = Input.GetKey(lowerElevationInput);
		bool raiseElevation = Input.GetKey(raiseElevationInput);
		if (lowerElevation != raiseElevation)
		{
			elevationFromTarget += (raiseElevation ? movementSpeed : -movementSpeed) * Time.deltaTime;
		}

		// Update our position.
		Vector3 offsetPosition = transform.position - target.position;
		offsetPosition.y = 0.0f;
		offsetPosition.Normalize();
		transform.position = target.position + offsetPosition * distanceFromTarget + Vector3.up * elevationFromTarget;
		transform.LookAt(target);

		// Left/right rotation.
		bool moveLeft = Input.GetKey(leftRotationInput);
		bool moveRight = Input.GetKey(rightRotationInput);
		if (moveLeft != moveRight)
		{
			float rotation = (moveLeft ? rotationSpeed : -rotationSpeed) * Time.deltaTime;
			transform.RotateAround(target.position, Vector3.up, rotation);
		}
	}
}