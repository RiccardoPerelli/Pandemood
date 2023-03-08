using Enigma_Rabbia;
using UnityEngine;

namespace Character.Character_Controller
{
    public class PhysicsCharacterController : MonoBehaviour
    {

        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool _isReferenceSystemInverted = false;

        private Vector3 m_Velocity = Vector3.zero;

        private Rigidbody _Rigidbody;

        private bool _isJumping = false;
        private bool _isGrounding = true;
        private bool _canClimb = false;

        private float _hValue;
        private float _rigidbodySpeed = 0.2f;

        private int _inversionMultiplier = 1;

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
        public float elevatorJumpMultiplier = 3f;
        public float smallFormParametersDivider = 3f;
    
        public string jumpButton = "Jump";
        public AnimationCharacterController animationController;
        [SerializeField] private CheckGrounding _checkGrounding;
    
        float turnSmoothVelocity;

        // Start is called before the first frame update
        void Start()
        {
            _Rigidbody = GetComponent<Rigidbody>();
            _checkGrounding.OnGrounding += Grounding;
            _checkGrounding.OnFalling += Falling;
            ChangeDirection.OnDirectionChanged += OnChangeDirection;
            Rimpicciolimento.SmallTriggered += SmallParameters;
            Rimpicciolimento.BigTriggered += BigParameters;
            if (_isReferenceSystemInverted)
            {
                _inversionMultiplier = -1;
            }
        }

        void OnChangeDirection(string corner)
        {
            isRunningOnZ = !isRunningOnZ;
            if (isRunningOnZ)
            {
                _Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | 
                    RigidbodyConstraints.FreezeRotationY;
            } 
            else
            {
                _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX |
                    RigidbodyConstraints.FreezeRotationY;
            }
            switch(corner) {
                case "Corner":
                    if (_inversionMultiplier == 1)
                    {
                        _inversionMultiplier = -1;
                    }
                    else
                    {
                        _inversionMultiplier = 1;
                    }
                    break;
                case "Corner1":
                    break;
                case "Corner2":
                    if(_inversionMultiplier == 1)
                    {
                        _inversionMultiplier = -1;
                    } else
                    {
                        _inversionMultiplier = 1;
                    }
                    break;
                case "Corner3":
                    break;
                default:
                    break;
            }
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

        void SmallParameters()
        {
            pushingSpeed /= smallFormParametersDivider;
            jumpStrength /= 1.6f;
            jumpHorizontalSpeed /= smallFormParametersDivider;
            runSpeed /= smallFormParametersDivider;
            climbSpeed /= smallFormParametersDivider;
        }

        void BigParameters()
        {
            pushingSpeed *= smallFormParametersDivider;
            jumpStrength *= 1.6f;
            jumpHorizontalSpeed *= smallFormParametersDivider;
            runSpeed *= smallFormParametersDivider;
            climbSpeed *= smallFormParametersDivider;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(jumpButton)) {
                _isJumping = true;
            }
            _hValue = Input.GetAxisRaw("Horizontal");
            _hValue *= _inversionMultiplier;
        }

        private void FixedUpdate()
        {
            float horizontalMove = _hValue * runSpeed;
            _rigidbodySpeed = GetComponent<Rigidbody>().velocity.magnitude;

            if(_rigidbodySpeed > pushingSpeed)
            {
                animationController.IsPushing = false;
            }

            if (!isRunningOnZ)
            {
                Vector3 horizontalDirection = new Vector3(horizontalMove, 0f, 0f).normalized;
                MoveHorizontalX(horizontalMove, horizontalDirection);
            }
            else
            {
                Vector3 horizontalDirection = new Vector3(0f, 0f, horizontalMove).normalized;
                MoveHorizontalZ(horizontalMove, horizontalDirection);
            }
            Jumping();
        }

        private void Jumping()
        {
            if (_isJumping && _isGrounding)
            {
                if (_canClimb)
                {
                    _Rigidbody.AddForce(Vector3.up * jumpStrength * elevatorJumpMultiplier);
                }
                else
                {
                    _Rigidbody.AddForce(Vector3.up * jumpStrength);
                }
                _isJumping = false;
                _isGrounding = false;
            }
        }

        public void MoveHorizontalX(float move, Vector3 direction)
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

        public void MoveHorizontalZ(float move, Vector3 direction)
        {
            Vector3 targetVelocity;
            if (Mathf.Abs(_hValue) >= startsToMoveHValue)
            {
                targetVelocity = new Vector3(_Rigidbody.velocity.x, _Rigidbody.velocity.y, move * 10f);
                _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else
            {
                targetVelocity = new Vector3(_Rigidbody.velocity.x, _Rigidbody.velocity.y, 0f);
                _Rigidbody.velocity = Vector3.SmoothDamp(_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
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
}
