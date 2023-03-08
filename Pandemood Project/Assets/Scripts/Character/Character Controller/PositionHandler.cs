using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
    }
}
