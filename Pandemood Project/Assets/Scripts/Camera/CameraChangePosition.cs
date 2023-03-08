using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangePosition : MonoBehaviour
{
    public CameraFollowTarget cameraScript;
    public Transform destinationPosition;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
            //invoke camera script to change position to target
            cameraScript.ChangeCameraPosition(destinationPosition);
            Destroy(this);
    }
}
