using System;
using UnityEngine;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviourEx : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0f;              // Default damp time to change the animations based on current speed.
	public string jumpButton = "Jump";             // Default jump button.
	public string crouchButton = "Crouch";
	public string shrinkButton = "Shrink";
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.
	public float crouchSpeed = 0.4f;
	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private bool jump;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.
	private bool canVertical;
	public float pushPower;
	private CapsuleCollider verticalCollider;
	private Collision colliding;
	private bool isClimbing;
	private bool isShrink;
	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		groundedBool = Animator.StringToHash("Grounded");
		behaviourManager.GetAnim.SetBool(groundedBool, true);

		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;
		verticalCollider = this.gameObject.GetComponent<CapsuleCollider>();
		isClimbing = false;
		isShrink = false;
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		canVertical = this.gameObject.GetComponent<BasicBehaviour>().canVertical;
		// Get jump input
		if (!jump && Input.GetButtonDown(jumpButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			jump = true;
		}
		checkPush();
		climbManager();
		checkShrink();
	}

	private void checkShrink()
	{
		Vector3 originalScale = new Vector3(1, 1, 1);
		Vector3 shrinkScale = new Vector3(0.3f, 0.3f, 0.3f);
		if (gameObject.GetComponent<Animator>().GetBool("Transform"))
			if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Transforming"))
			{
				gameObject.GetComponent<Animator>().SetBool("Transform", false);
				isShrink = !isShrink;
			}
		if (Input.GetButton(shrinkButton) && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Transforming"))
		{
			gameObject.GetComponent<Animator>().SetBool("Transform", true);
			gameObject.GetComponent<ParticleSystem>().Play();
		}
		if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Transforming"))
		{
			if (!isShrink)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 2.5f * Time.deltaTime);
				if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
				{
					transform.localScale = originalScale;
					gameObject.GetComponent<ParticleSystem>().Stop();

				}
			}
			else if (isShrink)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, shrinkScale, 2.5f * Time.deltaTime);
				if (Vector3.Distance(transform.localScale, shrinkScale) < 0.01f)
				{
					transform.localScale = shrinkScale;
					gameObject.GetComponent<ParticleSystem>().Stop();
				}
			}
		}
	}

	private void climbManager()
	{
		GameObject player = gameObject;
		Rigidbody m_Rigidbody = player.GetComponent<Rigidbody>();
		Animator anim = player.GetComponent<Animator>();
		float v = Input.GetAxis("Vertical");
		if (isClimbing)
		{
			player.GetComponent<BasicBehaviour>().enabled = false;
			//player.GetComponent<ThirdPersonCharacter>().enabled = false;
			m_Rigidbody.useGravity = false;
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

			if (v != 0)
			{
				player.transform.Translate(Vector3.up * v * speed * Time.deltaTime);
				anim.SetBool("Climbing", true);
				anim.speed = 1f;
			}
			if (v == -1)
			{
				player.transform.Translate(Vector3.up * v * speed * Time.deltaTime);
				anim.SetBool("Climbing", true);
				anim.speed = 1f;
			}
			if (v == 0)
			{
				anim.speed = 0f;
			}
			if (Input.GetButton(jumpButton))
			{
				player.GetComponent<BasicBehaviour>().enabled = true;
				//player.GetComponent<ThirdPersonCharacter>().enabled = true;
				m_Rigidbody.useGravity = true;
				if (canVertical)
					m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				else if (!canVertical)
				{
					m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
				}
				anim.SetBool("Climbing", false);
				anim.SetBool("ClimbDown", false);
				transform.Translate(Vector3.back * 0.1f);
			}
		}

		else
		{
			//player.GetComponent<ThirdPersonUserControl>().enabled = true;
			player.GetComponent<BasicBehaviour>().enabled = true;
			anim.SetBool("Climbing", false);
			m_Rigidbody.useGravity = true;
			if (canVertical)
			{
				this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			}
			if (!canVertical)
				this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			//m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			anim.speed = 1f;
			//anim.SetBool("ClimbDown", false);

		}
	}

	private void checkPush()
	{
		if (gameObject.GetComponent<Animator>().GetBool("Push"))
		{
			verticalCollider.radius = 0.5f;
			if (!this.gameObject.GetComponent<BasicBehaviour>().IsMoving())
			{
				gameObject.GetComponent<Animator>().SetBool("Push", false);
				verticalCollider.radius = 0.29f;
			}
		}
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Call the basic movement manager.
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);

		// Call the jump manager.
		JumpManagement();
	}

	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
		// Start a new jump.
		if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded())
		{
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(jumpBool, true);
			// Is a locomotion jump?
			if (behaviourManager.GetAnim.GetFloat(speedFloat) >= 0)
			{
				// Temporarily change player friction to pass through obstacles.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0f;
				// Remove vertical velocity to avoid "super jumps" on slope ends.
				RemoveVerticalVelocity();
				// Set jump vertical impulse velocity.
				float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
				velocity = Mathf.Sqrt(velocity);
				behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
			}
		}
		// Is already jumping?
		else if (behaviourManager.GetAnim.GetBool(jumpBool))
		{
			// Keep forward movement while in the air.
			if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus() && behaviourManager.GetAnim.GetFloat(speedFloat) > 0)
			{
				behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * sprintSpeed, ForceMode.Acceleration);
			}
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				jump = false;
				behaviourManager.GetAnim.SetBool(jumpBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}

	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		if (!canVertical)
			vertical = 0;
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
			speed = sprintSpeed;
		}
		if (behaviourManager.IsCrouching())
		{
			speed = crouchSpeed;
		}

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	// Remove vertical rigidbody velocity.
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		//FIXARE PROBLEMA ROTAZIONE VERSO LA TELECAMERA
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		if (canVertical == false)
			vertical = 0;
		targetDirection = forward * vertical + right * horizontal;
		//targetDirection = new Vector3(gameObject.transform.position.x + vertical, gameObject.transform.position.y, gameObject.transform.position.z + horizontal);


		// Lerp current direction to calculated target direction.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// Slide on vertical obstacles
		if (behaviourManager.IsCurrentBehaviour(this.GetBehaviourCode()) && collision.GetContact(0).normal.y <= 0.1f)
		{
			GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
			GetComponent<CapsuleCollider>().material.staticFriction = 0f;
		}
		if (collision.gameObject.tag == "Pushable" && this.gameObject.GetComponent<Animator>().GetBool("Push"))
		{
			if (canVertical)
				collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			else if (!canVertical)
				collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
		}
	}
	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
		GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
		GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
		if (collision.gameObject.tag == "Pushable")
		{
			collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
		}

		colliding = null;
	}

	public void Jump()
	{
		this.jump = true;
	}

	private void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Pushable")
		{
			//Debug.Log("STO SPINGENDO");
			colliding = collider;
			if (collider.transform.position.y > gameObject.transform.position.y)
				this.gameObject.GetComponent<Animator>().SetBool("Push", true);
			//var pushDir = new Vector3(collider.transform.position.x, 0, collider.transform.position.z);
			// If you know how fast your character is trying to move,
			// then you can also multiply the push velocity by that.
			// Apply the push
			//collider.collider.attachedRigidbody.velocity = pushDir * pushPower;
		}

	}
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody hitRigidbody = hit.collider.attachedRigidbody;
		if (hit.moveDirection.y < -0.3f)
			return;
		if (hitRigidbody != null && hitRigidbody.isKinematic == false && this.gameObject.GetComponent<Animator>().GetBool("Push"))
		{
			//hitRigidbody.AddForceAtPosition(hit.moveDirection * (speed / hitRigidbody.mass), hit.point, ForceMode.VelocityChange);
			hitRigidbody.AddForce(hit.moveDirection * (speed * 8 / hitRigidbody.mass), ForceMode.VelocityChange);
		}
	}
	/*private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ladder" &&Input.GetAxis("Vertical")>0)
		{
			//gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y - 90, gameObject.transform.rotation.z));
			//gameObject.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, other.transform.position.z);
			//gameObject.transform.LookAt(other.transform);
			isClimbing = true;
		}
	}*/
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Ladder" && Input.GetAxis("Vertical") > 0 && other.gameObject.name == "Bottom")
		{
			if (!isClimbing)
			{
				Vector3 posizioneScala = other.transform.position;
				posizioneScala.y = 0;
				posizioneScala.x += 0.35f;
				//posizioneScala.z += 0.3f;
				gameObject.transform.position = posizioneScala;
				Vector3 nuovaRotazione = gameObject.transform.rotation.eulerAngles;
				nuovaRotazione.y += -90;
				gameObject.transform.rotation = Quaternion.Euler(nuovaRotazione);
				//gameObject.transform.rotation = Quaternion.LookRotation(other.gameObject.transform.parent.rotation.eulerAngles);
			}
			isClimbing = true;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ladder" && other.gameObject.name == "Top")
		{
			isClimbing = false;
			//gameObject.GetComponent<Animator>().speed = 1f;
			//transform.Translate(Vector3.forward * 0.1f);
		}
	}
}
