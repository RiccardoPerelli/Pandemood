using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBehaviour : MonoBehaviour
{

    public string playerParentName = "MaleProvaParent";
    public string playerName = "MaleProvaRic2";
    public float distanceFromPlayerThreshold = 1.8f;
    public float newCapsuleColliderRadius = 0.48f;

    public bool isWalkingOnZ = false;

    private float _playerDistance;
    private float _startingColliderDim;
    private GameObject _player;
    private Animator _playerAnimator;

    private bool _isIn = false;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAnimator = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<Animator>();
        _startingColliderDim = _player.GetComponent<CapsuleCollider>().radius;
        Debug.Log(_player.name);
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(_player.transform.position, transform.position) <= distanceFromPlayerThreshold && !_isIn)
        {
            //Counter++;
            _isIn = true;
            _player.GetComponent<CapsuleCollider>().radius = newCapsuleColliderRadius;
        } 
        else if(_isIn && Vector3.Distance(_player.transform.position, transform.position) > distanceFromPlayerThreshold)
        {
            _isIn = false;
            _player.GetComponent<CapsuleCollider>().radius = _startingColliderDim;
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
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                    } 
                    else
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                    }
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    if (isWalkingOnZ)
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX;
                    }
                    else
                    {
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
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
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
}
