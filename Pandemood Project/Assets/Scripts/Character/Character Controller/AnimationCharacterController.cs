using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Character_Controller
{
    public class AnimationCharacterController : MonoBehaviour
    {
        private Animator _animator;
        private float _hValue;
        private bool _isJumping = false;
        private bool _isGrounding = true;

        [Header("Movement")] [Range(0, 1f)] [SerializeField]
        public float runningThreshold = .73f;

        public float startsToRunDivider = 4f;
        public float walkingSpeedMultiplierMax = 1.5f;
        public float startsToWalkThreshold = 0.05f;
        public float walkingVolume = 0.5f;
        public float runningVolume = 0.8f;
        public string jumpButton = "Jump";
        public bool _isPushing = false;
        public bool IsRunningOnZ = true;
        [SerializeField] private CheckGrounding checkGrounding;
        [SerializeField] private float fallThreshold = 0.1f;
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int WalkMultiplier = Animator.StringToHash("WalkMultiplier");
        private static readonly int Pushing = Animator.StringToHash("IsPushing");
        [SerializeField] private bool dialogueOpen;

        [Header("Sounds")] [SerializeField] private AudioClip _runningSound;
        [SerializeField] private AudioClip _jumpingSound;
        [SerializeField] private AudioClip _walkingSoundFast;
        [SerializeField] private AudioClip _walkingSoundMedium;
        [SerializeField] private AudioClip _walkingSoundSlow;
        public bool isSwimming;

        [Header("Walking Sound Tresholds")] [Range(0, 1f)] [SerializeField]
        private float _fastRunningThreshold = 0.6f;

        [Range(0, 1f)] [SerializeField] private float _mediumRunningThreshold = 0.3f;

        private AudioSource _audioSource;
        private bool _hasAudioSource;


        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            checkGrounding.OnGrounding += Grounding;
            checkGrounding.OnFalling += Falling;
            if (transform.parent.gameObject.GetComponent<ChangeDirection>() != null)
            {
                transform.parent.gameObject.GetComponent<ChangeDirection>().OnDirectionChanged += TurnOf90Degrees;
            }

            _audioSource = gameObject.GetComponent<AudioSource>();
            _hasAudioSource = _audioSource != null;
        }

        // Update is called once per frame
        void Update()
        {
            if (inGameMenu.GameIsPaused)
            {
                _audioSource.Stop();
                return;
            }
            if (Input.GetButtonDown(jumpButton) && !isSwimming)
            {
                _isJumping = true;
            }

            if(inGameMenu.GameIsPaused && _audioSource.isPlaying)
            {
                _audioSource.Pause();
            }

            _hValue = Input.GetAxisRaw("Horizontal");
            SetMovementState(_hValue, false);
        }

        private void FixedUpdate()
        {
            Jumping();
            _animator.SetBool(Pushing, IsPushing);
            if (_isGrounding)
            {
                _animator.SetBool(IsFalling, false);
            }
        }

        void TurnOf90Degrees(string corner)
        {
            Vector3 angles;
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
                _animator.SetBool(IsFalling, true);
                _animator.Play($"Base Layer.jump_start", 0, 0.2f);
                _isJumping = false;
                _isGrounding = false;
                if (_hasAudioSource)
                {
                    if (_jumpingSound != null)
                    {
                        if (_audioSource.clip != _jumpingSound || !_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                        {
                            _audioSource.clip = _jumpingSound;
                            _audioSource.pitch = 1;
                            _audioSource.Play();
                            _audioSource.loop = false;
                        }
                    }
                }
            }
        }

        public void SetMovementState(float h, bool hasFalled)
        {
            if (Mathf.Abs(h) < startsToWalkThreshold)
            {
                if (hasFalled)
                {
                    _animator.SetBool(IsFalling, false);
                    _animator.CrossFade("Base Layer.Idle", 0.2f, 0);
                }

                _animator.SetBool(IsWalking, false);
                _animator.SetBool(IsRunning, false);

                if (_hasAudioSource && _isGrounding)
                    _audioSource.Pause();
            }
            else if (_isPushing)
            {
                if (_audioSource.clip != _walkingSoundSlow || !_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                {
                    _audioSource.clip = _walkingSoundSlow;
                    _audioSource.Play();
                    _audioSource.volume = walkingVolume;
                    _audioSource.loop = true;
                }
            }
            else if (Mathf.Abs(h) < runningThreshold)
            {
                if (hasFalled)
                {
                    _animator.SetBool(IsFalling, false);
                    _animator.CrossFade("Base Layer.walk_fwd", 0.2f, 0);
                }

                _animator.SetBool(IsWalking, true);
                _animator.SetBool(IsRunning, false);
                float normalizedH = (Mathf.Abs(h) - runningThreshold / startsToRunDivider) / runningThreshold + 1;
                _animator.SetFloat(WalkMultiplier, normalizedH);

                if (isSwimming)
                {
                    if (_audioSource.isPlaying)
                        _audioSource.Stop();
                }
                else if (_hasAudioSource && _isGrounding && !inGameMenu.GameIsPaused)
                {
                    if (Mathf.Abs(h) > _fastRunningThreshold)
                    {
                        if (_walkingSoundFast != null)
                        {
                            if (_audioSource.clip != _walkingSoundFast || !_audioSource.isPlaying)
                            {
                                _audioSource.clip = _walkingSoundFast;
                                _audioSource.Play();
                                _audioSource.volume = walkingVolume;
                                _audioSource.loop = true;
                            }
                        }
                    }
                    else if (Mathf.Abs(h) > _mediumRunningThreshold)
                    {
                        if (_walkingSoundMedium != null)
                        {
                            if (_audioSource.clip != _walkingSoundMedium || !_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                            {
                                _audioSource.clip = _walkingSoundMedium;
                                _audioSource.Play();
                                _audioSource.volume = walkingVolume;
                                _audioSource.loop = true;
                            }
                        }
                    }
                    else
                    {
                        if (_walkingSoundSlow != null)
                        {
                            if (_audioSource.clip != _walkingSoundSlow || !_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                            {
                                _audioSource.clip = _walkingSoundSlow;
                                _audioSource.Play();
                                _audioSource.volume = walkingVolume;
                                _audioSource.loop = true;
                            }
                        }
                    }
                }
            }
            else if (Mathf.Abs(h) > runningThreshold)
            {
                if (hasFalled)
                {
                    _animator.SetBool(IsFalling, false);
                    _animator.CrossFade("Base Layer.run_fwd", 0.2f, 0);
                }

                _animator.SetBool(IsWalking, false);
                _animator.SetBool(IsRunning, true);
                if (_hasAudioSource && _isGrounding)
                {
                    if (_runningSound != null)
                    {
                        if (_animator.GetBool(IsRunning) && _audioSource.clip != _runningSound ||
                            !_audioSource.isPlaying && !inGameMenu.GameIsPaused)
                        {
                            _audioSource.pitch = 1;
                            _audioSource.clip = _runningSound;
                            _audioSource.volume = runningVolume;
                            _audioSource.Play();
                            _audioSource.loop = true;
                        }
                    }
                }
            }
        }

        public bool IsPushing
        {
            get => _isPushing; // get method
            set => _isPushing = value; // set method
        }

        void Grounding()
        {
            _isGrounding = true;
            _isJumping = false;
            if (dialogueOpen)
                SetMovementState(0, true);
            else
                SetMovementState(_hValue, true);
        }

        public void SetDialogueOpen(bool val)
        {
            dialogueOpen = val;
        }

        void Falling()
        {
            _isGrounding = false;
            StartCoroutine(StartFalling());
        }

        private IEnumerator StartFalling()
        {
            yield return new WaitForSeconds(fallThreshold);
            if (!_isGrounding)
                _animator.SetBool(IsFalling, true);
        }
    }
}