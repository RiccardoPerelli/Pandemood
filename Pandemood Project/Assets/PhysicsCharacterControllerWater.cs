using System;
using System.Collections;
using System.Collections.Generic;
using DDSystem.Script;
using Doublsb.Dialog;
using UnityEngine;
using Object = UnityEngine.Object;

public class PhysicsCharacterControllerWater : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f; // How much to smooth out the movement

    private Rigidbody _rigidbody;

    private float _hvalue;
    private float _vvalue;
    private float _horizontalMove;
    private float _verticalMove;

    private Vector3 _horizontalDirection;
    private Vector3 _verticalDirection;
    private Vector3 _velocity = Vector3.zero;

    public float SwimSpeed = 0.8f;
    public float StartsToMoveHValue = 0.2f;
    public float turnSmoothtime = 0.1f;

    float turnSmoothVelocity;

    private DialogManager[] dialogManager;
    private bool _dialogOpen;

    public PhysicsCharacterControllerWater(DialogManager[] dialogManager)
    {
        this.dialogManager = dialogManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;
        dialogManager = Resources.FindObjectsOfTypeAll<DialogManager>();
        foreach (var dialog in dialogManager)
        {
            dialog.OnActivate += OnActivateDialogue;
            dialog.OnDeactivate += OnDisableDialogue;
        }
    }

    void OnActivateDialogue(object p, EventArgs d)
    {
        _dialogOpen = true;
    }

    void OnDisableDialogue(object p, EventArgs d)
    {
        _dialogOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        _hvalue = !_dialogOpen ? Input.GetAxisRaw("Horizontal") : 0;
        _vvalue = !_dialogOpen ? Input.GetAxisRaw("Vertical") : 0;
        Debug.Log(_vvalue);
    }

    private void FixedUpdate()
    {
        _horizontalMove = _hvalue * SwimSpeed;
        _verticalMove = _vvalue * SwimSpeed;
        _horizontalDirection = new Vector3(_horizontalMove, 0f, 0f).normalized;
        _verticalDirection = new Vector3(0f, _verticalMove, 0f).normalized;
        MoveHorizontal(_horizontalMove);
        MoveVertical(_verticalMove);
        MangageAngVelocity(_hvalue);
        ChangeDirection(_verticalDirection, _horizontalDirection);
    }

    public void MoveHorizontal(float move)
    {
        Vector3 targetVelocity;
        if (Mathf.Abs(_hvalue) >= StartsToMoveHValue)
        {
            var velocity = _rigidbody.velocity;
            targetVelocity = new Vector3(move * 10f, velocity.y, velocity.z);
            _rigidbody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, m_MovementSmoothing);
        }
        else
        {
            var velocity = _rigidbody.velocity;
            targetVelocity = new Vector3(0f, velocity.y, velocity.z);
            _rigidbody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, m_MovementSmoothing);
        }
    }

    public void MoveVertical(float move)
    {
        Vector3 targetVelocity;
        var velocity = _rigidbody.velocity;
        targetVelocity = new Vector3(velocity.x, move * 10f, velocity.z);
        _rigidbody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, m_MovementSmoothing);
    }

    public void ChangeDirection(Vector3 verticalDirection, Vector3 horizontalDirection)
    {
        if (verticalDirection.magnitude >= 0.01f || horizontalDirection.magnitude >= 0.01f)
        {
            var eulerAngles = transform.eulerAngles;
            float angleV = Mathf.SmoothDampAngle(eulerAngles.x, _vvalue * -55, ref turnSmoothVelocity, turnSmoothtime);
            float angleH = Mathf.SmoothDampAngle(eulerAngles.y, _hvalue * 90, ref turnSmoothVelocity, turnSmoothtime);
            transform.rotation = Quaternion.Euler(angleV, angleH, 0);
        }
    }

    private void MangageAngVelocity(float h)
    {
        if (Mathf.Abs(h) < 0.05f)
        {
            if (_rigidbody.angularVelocity.magnitude > 0.1f)
            {
                _rigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }
    }
}