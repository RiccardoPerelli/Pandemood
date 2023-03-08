using System;
using UnityEngine;
// FlyBehaviour inherits from GenericBehaviour. This class corresponds to the flying behaviour.
public class SwimBehaviour : GenericBehaviour
{
	//public string swimButton = "Fly";              // Default fly button.
	public float swimSpeed = 4.0f;                 // Default flying speed.
	public float sprintFactor = 2.0f;             // How much sprinting affects fly speed.
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
	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		swimBool = Animator.StringToHash("Swim");
		col = this.GetComponent<CapsuleCollider>();
		// Subscribe this behaviour on the manager.
		behaviourManager.SubscribeBehaviour(this);
		timeBreath = tempoInAcqua;
		inWater = false;
		WaterLevel = GameObject.Find("WaterLevel").transform.position.y;
		CharHead = GameObject.Find("CharHead").transform.position.y;
	}

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
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
			if(inWater)
			behaviourManager.GetRigidBody.useGravity = !swim;

			// Player is flying.
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
		if (swim&&IsUnderwater()&&tempoInAcqua > 0)
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

			// Set fly related variables on the Animator Controller.
			behaviourManager.GetAnim.SetBool(swimBool, swim);
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

		if(Input.GetButton(jumpButton)&&WaterLevel+gameObject.transform.position.y<waterSurfacePosition)
		{
		  targetDirection.y += 0.3f;
		}
		else if(Input.GetButton(jumpButton) && WaterLevel + gameObject.transform.position.y == waterSurfacePosition)
        {
			targetDirection.y += 0;
		}
		if (Input.GetButton(crouchButton))
		{
			targetDirection.y -= 0.3f;
		}
		// Rotate the player to the correct fly position.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);

			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}

		// Player is flying and idle?
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
		return isUnderwater = (CharHead+gameObject.transform.position.y) <= waterSurfacePosition;
    }

	private void OnTriggerEnter(Collider other)
    {
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
		{
			waterSurface = other.transform;
			waterSurfacePosition = waterSurface.position.y;
			//Debug.Log(waterSurfacePosition);
			if ((WaterLevel + gameObject.transform.position.y) <= waterSurfacePosition)
			inWater = true;
		}
    }
    private void OnTriggerExit(Collider other)
    {
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water" && !IsUnderwater()&& (WaterLevel + gameObject.transform.position.y) > waterSurfacePosition+0.1f)
		{
			inWater = false;
            //waterSurfacePosition = 0f;
		}
	}


}
