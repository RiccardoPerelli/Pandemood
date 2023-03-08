using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBehaviour : MonoBehaviour
{

    public GameObject player;
    public Animator playerAnimator;
    public float distanceFromPlayerThreshold = 1.8f;
    public float newCapsuleColliderRadius = 0.48f;
    private float _playerDistance;
    private float _startingColliderDim;

    private void Start()
    {
        _startingColliderDim = player.GetComponent<CapsuleCollider>().radius;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distanceFromPlayerThreshold)
        {
            player.GetComponent<CapsuleCollider>().radius = newCapsuleColliderRadius;
        } 
        else
        {
            player.GetComponent<CapsuleCollider>().radius = _startingColliderDim;
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
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pushing"))
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                    } else
                    {
                        GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
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
}
