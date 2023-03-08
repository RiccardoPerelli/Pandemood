using System;
using UnityEngine;
// FlyBehaviour inherits from GenericBehaviour. This class corresponds to the flying behaviour.
public class SwimBehaviour : GenericBehaviour
{
	//public string swimButton = "Fly";              // Default fly button.
	public float swimSpeed = 4.0f;                 // Default flying speed.
	public float sprintFactor = 2.0f;             // How much sprinting affects fly speed.
	public float ySprint;
												  //public float flyMaxVerticalAngle = 60f;       // Angle to clamp camera vertical movement when flying.
	public string jumpButton = "Jump";
	public string crouchButton = "Crouch";
	private int swimBool;                          // Animator variable related to flying.
	private bool swim = false;                     // Boolean to determine whether or not the player activated fly mode.
	public bool inWater;
	private CapsuleCollider col;                  // Reference to the player capsulle collider.
	public float tempoInAcqua;
	private float timeBreath;
	private float waterSurfacePosition = 0.0f;
	private Transform waterSurface;
	private AudioSource swimmingAudio;
	private float WaterLevel;
	private float CharHead;
	public bool isUnderwater;
	private int direction;
	public bool checkWater;
	private int prevColDir;
	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		direction = Animator.StringToHash("Direction");
		swimBool = Animator.StringToHash("Swim");
		col = this.GetComponent<CapsuleCollider>();
		// Subscribe this behaviour on the manager.
		behaviourManager.SubscribeBehaviour(this);
		timeBreath = tempoInAcqua;
		inWater = false;
		WaterLevel = GameObject.Find("WaterLevel").transform.position.y;
		CharHead = GameObject.Find("CharHead").transform.position.y;
		waterSurface = GameObject.Find("WaterSurface").transform;
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		if (checkWater)
		{
			waterSurfacePosition = waterSurface.position.y;
			if ((WaterLevel) <= waterSurfacePosition)
			{//
				inWater = true;
				this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			}
			if ((WaterLevel) >= waterSurfacePosition)
			{
				prevColDir = col.direction;
				col.direction = 1;
				if (this.gameObject.GetComponent<BasicBehaviour>().IsGrounded())
				{
					inWater = false;
					checkWater = false;
				}
				else
					col.direction = prevColDir;
			}
			//Debug.Log(CharHead);
			if (!inWater)
			{
				swim = false;
				//behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

				// Obey gravity. It's the law!
				behaviourManager.GetRigidBody.useGravity = !swim;
				tempoInAcqua = timeBreath;
				col.direction = 1;
				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
			// Toggle fly by input, only if there is no overriding state or temporary transitions.
			if (inWater && !behaviourManager.IsOverriding()
				&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
			{
				swim = true;

				// Force end jump transition.
				behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

				// Obey gravity. It's the law!
				if (inWater)
					behaviourManager.GetRigidBody.useGravity = !swim;

				// Player is swimming.
				if (swim)
				{
					// Register this behaviour.
					behaviourManager.RegisterBehaviour(this.behaviourCode);
				}
				else
				{
					// Set collider direction to vertical.
					col.direction = 1;
					// Set camera default offset.
					//behaviourManager.GetCamScript.ResetTargetOffsets();

					// Unregister this behaviour and set current behaviour to the default one.
					behaviourManager.UnregisterBehaviour(this.behaviourCode);
				}
			}
			if (swim && IsUnderwater() && tempoInAcqua > 0)
			{
				tempoInAcqua -= Time.deltaTime;
			}
			if (swim && IsUnderwater() && tempoInAcqua <= 0)
			{
				swim = false;
				//Game over?
				// Force end jump transition.
				behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

				// Obey gravity. It's the law!
				behaviourManager.GetRigidBody.useGravity = !swim;
				tempoInAcqua = timeBreath;
				col.direction = 1;
				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
			// Assert this is the active behaviour
			swim = swim && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

			// Set swim related variables on the Animator Controller.
			behaviourManager.GetAnim.SetBool(swimBool, swim);
		}
	}

	// This function is called when another behaviour overrides the current one.
	public override void OnOverride()
	{
		// Ensure the collider will return to vertical position when behaviour is overriden.
		col.direction = 1;
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Set camera limit angle related to fly mode.
		//behaviourManager.GetCamScript.SetMaxVerticalAngle(flyMaxVerticalAngle);

		// Call the fly manager.
		SwimManagement(behaviourManager.GetH, behaviourManager.GetV);
	}
	// Deal with the player movement when flying.
	void SwimManagement(float horizontal, float vertical)
	{
		// Add a force player's rigidbody according to the fly direction.
		Vector3 direction = Rotating(horizontal, vertical);
		behaviourManager.GetRigidBody.AddForce((direction * swimSpeed * 100 * (behaviourManager.IsSprinting() ? sprintFactor : 1)), ForceMode.Acceleration);
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		forward = forward.normalized;
		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		// Calculate target direction based on camera forward and direction key.
		Vector3 targetDirection = forward * vertical + right * horizontal;

		if (Input.GetButton(jumpButton) && WaterLevel< waterSurfacePosition)
		{
			targetDirection.y += ySprint;
			//behaviourManager.GetAnim.SetFloat(direction, +0.1f);
		}
		else if (Input.GetButton(jumpButton) && WaterLevel == waterSurfacePosition)
		{
			targetDirection.y += 0;
		}
		if (Input.GetButton(crouchButton))
		{
			targetDirection.y -= ySprint/1.2f;
			//behaviourManager.GetAnim.SetFloat(direction, -0.1f);
		}
		// Rotate the player to the correct fly position.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			//behaviourManager.GetAnim.SetFloat(direction, 0f);
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);

			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}

		// Player is swimming and idle?
		if (!(Mathf.Abs(horizontal) > 0.2 || Mathf.Abs(vertical) > 0.2))
		{
			// Rotate the player to stand position.
			behaviourManager.Repositioning();
			// Set collider direction to vertical.
			col.direction = 1;
		}
		else
		{
			// Set collider direction to horizontal.
			col.direction = 2;
		}

		// Return the current fly direction.
		return targetDirection;
	}

	public bool IsUnderwater()
	{
		return isUnderwater = (CharHead + gameObject.transform.position.y) <= waterSurfacePosition;
	}

	/*private void OnTriggerEnter(Collider other)
    {
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
		{
			waterSurface = other.transform;
			waterSurfacePosition = waterSurface.position.y;
			checkWater = true;
			//Debug.Log(waterSurfacePosition);
			//Debug.Log(WaterLevel + gameObject.transform.position.y);
			//if ((WaterLevel + gameObject.transform.position.y) <= waterSurfacePosition) //&& !this.gameObject.GetComponent<BasicBehaviour>().IsGrounded())
			//inWater = true;
		}
    }*/
	private void OnTriggerStay(Collider other)
	{
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
		{
			waterSurface = other.transform;
			waterSurfacePosition = waterSurface.position.y;
			checkWater = true;
			/*if (LayerMask.LayerToName(other.gameObject.layer) == "Water" && IsUnderwater() == false && ((WaterLevel + gameObject.transform.position.y) > waterSurfacePosition + 0.1f))
			{
				prevColDir = col.direction;
				col.direction = 1;
				if (this.gameObject.GetComponent<BasicBehaviour>().IsGrounded())
				{
					inWater = false;
					checkWater = false;
				}
				else
					col.direction = prevColDir;
				//waterSurfacePosition = 0f;
			}*/
		}


	}
}
