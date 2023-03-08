using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterControllerWater : MonoBehaviour
{

    [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private Rigidbody _Rigidbody;

    private float _Hvalue;
    private float _Vvalue;
    private float _HorizontalMove;
    private float _VerticalMove;

    private Vector3 _HorizontalDirection;
    private Vector3 _VerticalDirection;
    private Vector3 _Velocity = Vector3.zero;

    public float SwimSpeed = 0.8f;
    public float StartsToMoveHValue = 0.2f;
    public float turnSmoothtime = 0.1f;

    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _Hvalue = Input.GetAxisRaw("Horizontal");
        _Vvalue = Input.GetAxisRaw("Vertical");
        Debug.Log(_Vvalue);
    }

    private void FixedUpdate()
    {
        _HorizontalMove = _Hvalue * SwimSpeed;
        _VerticalMove = _Vvalue * SwimSpeed;
        _HorizontalDirection = new Vector3(_HorizontalMove, 0f, 0f).normalized;
        _VerticalDirection = new Vector3(0f, _VerticalMove, 0f).normalized;
        MoveHorizontal(_HorizontalMove);
        MoveVertical(_VerticalMove);
        MangageAngVelocity(_Hvalue);
        ChangeDirection(_VerticalDirection, _HorizontalDirection);
    }

    public void MoveHorizontal(float move)
    {
        Vector3 targetVelocity;
        if (Mathf.Abs(_Hvalue) >= StartsToMoveHValue)
        {
            targetVelocity = new Vector3(move * 10f, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref _Velocity, m_MovementSmoothing);
        }
        else
        {
            targetVelocity = new Vector3(0f, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref _Velocity, m_MovementSmoothing);
        }
    }
    public void MoveVertical(float move)
    {
        Vector3 targetVelocity;
        targetVelocity = new Vector3(_Rigidbody.velocity.x, move * 10f, _Rigidbody.velocity.z);
        _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref _Velocity, m_MovementSmoothing);
    }

    public void ChangeDirection(Vector3 verticalDirection, Vector3 horizontalDirection)
    {
        if (verticalDirection.magnitude >= 0.01f || horizontalDirection.magnitude >= 0.01f)
        {
            float angleV = Mathf.SmoothDampAngle(transform.eulerAngles.x, _Vvalue * -90, ref turnSmoothVelocity, turnSmoothtime);
            float angleH = Mathf.SmoothDampAngle(transform.eulerAngles.y, _Hvalue * 90, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(angleV, angleH, 0);
        }
    }

    private void MangageAngVelocity(float h)
    {
        if (Mathf.Abs(h) < 0.05f)
        {
            if (_Rigidbody.angularVelocity.magnitude > 0.1f)
            {
                _Rigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }
    }
}
