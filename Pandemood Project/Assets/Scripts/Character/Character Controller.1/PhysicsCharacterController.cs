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

    public bool isRunningOnZ = true;

    public float turnSmoothtime = 0.1f;
    public float runSpeed = 40f;
    public float jumpStrength = 20000f;
    public float jumpHorizontalSpeed = 3.5f;
    public float yVelocityThreshold = 10f;
    
    public string jumpButton = "Jump";
    
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
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float horizontalMove = h * runSpeed;

        Vector3 direction = new Vector3(horizontalMove, 0f, 0f).normalized;

        Move(horizontalMove, direction);
        MangageAngVelocity(h);
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

    public void Move(float move, Vector3 direction)
    {
        Vector3 targetVelocity;
        targetVelocity = new Vector3(move * 10f, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
        _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
