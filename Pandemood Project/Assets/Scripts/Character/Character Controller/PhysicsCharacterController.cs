using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private Vector3 m_Velocity = Vector3.zero;

    private Rigidbody _Rigidbody;

    private bool _isJumping = false;
    private bool _isGrounding = true;
    private bool _canClimb = false;

    private float _hValue;
    private float _rigidbodySpeed = 0.2f;

    public bool isRunningOnZ = true;

    public float turnSmoothtime = 0.1f;
    public float runSpeed = 0.8f;
    public float climbSpeed = 0.8f;
    public float jumpStrength = 20000f;
    public float jumpHorizontalSpeed = 3.5f;
    public float yVelocityThreshold = 10f;
    public float yDistanceFromObstacle = 0.3f;
    public float pushingSpeed = 1.3f;
    public float startsToMoveHValue = 0.2f;
    
    public string jumpButton = "Jump";
    public AnimationCharacterController animationController;
    
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        CheckGrounding.OnGrounding += Grounding;
        CheckGrounding.OnFalling += Falling;
    }

    void Grounding()
    {
        _isGrounding = true;
        _isJumping = false;
    }

    void Falling()
    {
        _isGrounding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpButton)) {
            _isJumping = true;
        }
        _hValue = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float horizontalMove = _hValue * runSpeed;
        _rigidbodySpeed = GetComponent<Rigidbody>().velocity.magnitude;

        if (_canClimb) {
            float v = Input.GetAxisRaw("Vertical");
            if (v >= 0.1f)
            {
                float verticalMove = v * climbSpeed;
                Vector3 verticalDirection = new Vector3(0f, verticalMove, 0f).normalized;
                MoveVertical(verticalMove, verticalDirection);
            }
        }

        if(_rigidbodySpeed > pushingSpeed)
        {
            animationController.IsPushing = false;
        }

        Vector3 horizontalDirection = new Vector3(horizontalMove, 0f, 0f).normalized;

        MoveHorizontal(horizontalMove, horizontalDirection);
        MangageAngVelocity(_hValue);
        Jumping();
    }

    private void MangageAngVelocity(float h)
    {
        if (h < 0.05f)
        {
            if (_Rigidbody.angularVelocity.magnitude > 0.1f)
            {
                _Rigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }
    }

    private void Jumping()
    {
        if (_isJumping && _isGrounding)
        {
            _Rigidbody.AddForce(Vector3.up * jumpStrength);
            _isJumping = false;
            _isGrounding = false;
        }
    }

    public void MoveHorizontal(float move, Vector3 direction)
    {
        Vector3 targetVelocity;
        if (Mathf.Abs(_hValue) >= startsToMoveHValue)
        {
            targetVelocity = new Vector3(move * 10f, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        } else
        {
            targetVelocity = new Vector3(0f, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
    public void MoveVertical(float move, Vector3 direction)
    {
        Vector3 targetVelocity;
        targetVelocity = new Vector3(_Rigidbody.velocity.x, move * 10f, _Rigidbody.velocity.z);
        _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            _canClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _canClimb = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.CompareTag("Pushable"))
            {
                if (Mathf.Abs(_hValue) >= 0.35f && Mathf.Abs((collision.transform.position.y - 0.5f) - transform.position.y) <= yDistanceFromObstacle)
                {
                    animationController.IsPushing = true;
                }
                else
                {
                    animationController.IsPushing = false;
                }
            }
        }
    }
}
