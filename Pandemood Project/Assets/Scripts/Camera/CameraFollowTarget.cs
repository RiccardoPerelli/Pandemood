using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;
    [Range(1f, 40f)] public float laziness = 10f;
    public bool lookAtTarget = true;
    public bool takeOffsetFromInitialPos = true;
    public Vector3 fromInitialPositionOffset;
    public Vector3 generalOffset;
    public Vector3 smallDistance;
    public Vector3 bigDistance;
    public float ySmallDistance = 1f;
    public float zSmalldistance = 1f;
    Vector3 whereCameraShouldBe;
    bool warningAlreadyShown = false;

    private void Start()
    {
        if (takeOffsetFromInitialPos && target != null) generalOffset = transform.position - target.position -fromInitialPositionOffset;
        bigDistance = generalOffset;
        if(smallDistance.y == 0)
        {
            smallDistance = new Vector3(bigDistance.x, ySmallDistance, zSmalldistance);
        }
        Rimpicciolimento.SmallTriggered += ResetSmallCameraPosition;
        Rimpicciolimento.BigTriggered += ResetBigCameraPosition;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            whereCameraShouldBe = target.position + generalOffset;
            transform.position = Vector3.Lerp(transform.position, whereCameraShouldBe, 1 / laziness);

            if (lookAtTarget) transform.LookAt(target);
        }
        else
        {
            if (!warningAlreadyShown)
            {
                Debug.Log("Warning: You should specify a target in the simpleCamFollow script.", gameObject);
                warningAlreadyShown = true;
            }
        }
    }

    void ResetSmallCameraPosition()
    {
        generalOffset = smallDistance;
    }
    void ResetBigCameraPosition()
    {
        generalOffset = bigDistance;
    }
}
