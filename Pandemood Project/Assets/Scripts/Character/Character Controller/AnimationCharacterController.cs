using UnityEngine;

namespace Character.Character_Controller
{
    public class AnimationCharacterController : MonoBehaviour
    {
        private Animator _animator;
        private float _hValue;
        private bool _isJumping = false;
        private bool _isGrounding = true;

        [Range(0, 1f)] [SerializeField] public float runningThreshold = .6f;
        public float startsToRunDivider = 10f;
        public float walkingSpeedMultiplierMax = 1.5f;
        public string jumpButton = "Jump";
        public bool _isPushing = false;
        public bool IsRunningOnZ = true;
        [SerializeField] private CheckGrounding checkGrounding;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            checkGrounding.OnGrounding += Grounding;
            checkGrounding.OnFalling += Falling;
            ChangeDirection.OnDirectionChanged += TurnOf90Degrees;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(jumpButton))
            {
                _isJumping = true;
            }
            _hValue = Input.GetAxisRaw("Horizontal");
            SetMovementState(_hValue, false);
        }

        private void FixedUpdate()
        {
            Jumping();
            _animator.SetBool("IsPushing", IsPushing);
            if (_isGrounding)
            {
                _animator.SetBool("IsFalling", false);
            }
        }

        void TurnOf90Degrees(string corner)
        {
            Vector3 angles = transform.eulerAngles;
            float y;
            switch (corner)
            {
                case "Corner":
                    angles = transform.eulerAngles;
                    if (IsRunningOnZ)
                    {
                        y = angles.y + 90f;
                        IsRunningOnZ = false;
                    }
                    else
                    {
                        y = angles.y - 90f;
                        IsRunningOnZ = true;
                    }
                    transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
                    break;

                case "Corner1":
                    angles = transform.eulerAngles;
                    if (IsRunningOnZ)
                    {
                        y = angles.y + 90f;
                        IsRunningOnZ = false;
                    }
                    else
                    {
                        y = angles.y - 90f;
                        IsRunningOnZ = true;
                    }
                    transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
                    break;

                case "Corner2":
                    angles = transform.eulerAngles;
                    if (IsRunningOnZ)
                    {
                        y = angles.y + 90f;
                        IsRunningOnZ = false;
                    }
                    else
                    {
                        y = angles.y - 90f;
                        IsRunningOnZ = true;
                    }
                    transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
                    break;

                case "Corner3":
                    angles = transform.eulerAngles;
                    if (IsRunningOnZ)
                    {
                        y = angles.y + 90f;
                        IsRunningOnZ = false;
                    }
                    else
                    {
                        y = angles.y - 90f;
                        IsRunningOnZ = true;
                    }
                    transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
                    break;

                default:
                    Debug.Log("String invalid");
                    break;
            }
        }

        private void Jumping()
        {
            if (_isJumping && _isGrounding)
            {
                _animator.SetBool("IsFalling", true);
                _animator.Play("Base Layer.jump_start", 0, 0.2f);
                _isJumping = false;
                _isGrounding = false;
            }
        }

        private void SetMovementState(float h, bool hasFalled)
        {
            if (Mathf.Abs(h) < runningThreshold / startsToRunDivider)
            {
                if (hasFalled)
                {
                    _animator.SetBool("IsFalling", false);
                    _animator.CrossFade("Base Layer.Idle", 0.2f, 0);
                }
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
            }
            else if (Mathf.Abs(h) < runningThreshold)
            {
                if (hasFalled)
                {
                    _animator.SetBool("IsFalling", false);
                    _animator.CrossFade("Base Layer.walk_fwd", 0.2f, 0);
                }
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsRunning", false);
                float normalizedH = (Mathf.Abs(h) - runningThreshold / startsToRunDivider) / runningThreshold + 1;
                _animator.SetFloat("WalkMultiplier", normalizedH);
            }
            else if (Mathf.Abs(h) > runningThreshold)
            {
                if (hasFalled)
                {
                    _animator.SetBool("IsFalling", false);
                    _animator.CrossFade("Base Layer.run_fwd", 0.2f, 0);
                }
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", true);
            }
        }

        public bool IsPushing
        {
            get { return _isPushing; }   // get method
            set { _isPushing = value; }  // set method
        }

        void Grounding()
        {
            _isGrounding = true;
            _isJumping = false;
            SetMovementState(_hValue, true);
        }

        void Falling()
        {
            _isGrounding = false;
            _animator.SetBool("IsFalling", true);
        }
    }
}
