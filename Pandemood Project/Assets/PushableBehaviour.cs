using Character.Character_Controller;
using UnityEngine;

public class PushableBehaviour : MonoBehaviour
{

    public string playerParentName = "MaleProvaParent";
    public string playerName = "MaleProvaRic2";
    public float distanceFromPlayerThreshold = 1.8f;
    public float newCapsuleColliderRadius = 0.48f;

    public bool isWalkingOnZ;

    private CapsuleCollider _playerCollider;
    private float _playerDistance;
    private float _startingColliderDim;
    private GameObject _player;
    private Rigidbody _rigidbody;
    private bool _isIn;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<Animator>();
        _startingColliderDim = _player.GetComponent<CapsuleCollider>().radius;
        Debug.Log(_player.name);
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = _player.GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(_player.transform.position, transform.position) <= distanceFromPlayerThreshold && !_isIn)
        {
            //Counter++;
            _isIn = true;
            _playerCollider.radius = newCapsuleColliderRadius;
        } 
        else if(_isIn && Vector3.Distance(_player.transform.position, transform.position) > distanceFromPlayerThreshold)
        {
            _isIn = false;
            _playerCollider.radius = _startingColliderDim;
        }
        if(_player.transform.GetChild(0).GetComponent<AnimationCharacterController>() != null && 
            _player.transform.GetChild(0).GetComponent<Animator>().GetBool("IsPushing") && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        } else if (GetComponent<AudioSource>().isPlaying && !_player.transform.GetChild(0).GetComponent<Animator>().GetBool("IsPushing"))
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void Update()
    {
        if (inGameMenu.GameIsPaused)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.CompareTag("Player"))
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.95f)
                {
                    if (isWalkingOnZ)
                    {
                        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    } 
                    else
                    {
                        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }
                }
                else
                {
                    _rigidbody.isKinematic = false;
                    if (isWalkingOnZ)
                    {
                        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    }
                    else
                    {
                        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.rigidbody.CompareTag("Player"))
            {
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
