using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeLumineo : MonoBehaviour
{
    [SerializeField] public GameObject lumineo;
    
    private void OnTriggerEnter(Collider other)
    {
        lumineo.SetActive(true);
    }
    
}
