using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public GameObject parent;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position + offset;
        transform.rotation = parent.transform.rotation;
    }
}
