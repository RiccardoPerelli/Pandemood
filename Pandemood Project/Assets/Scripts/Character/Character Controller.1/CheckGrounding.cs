using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounding : MonoBehaviour
{

    public delegate void Falling();
    public static event Falling OnFalling;
    public delegate void Grounding();
    public static event Grounding OnGrounding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (OnGrounding != null)
                OnGrounding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (OnFalling != null)
                OnFalling();
        }
    }
}
