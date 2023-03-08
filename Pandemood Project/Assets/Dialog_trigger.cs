using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_trigger : MonoBehaviour
{
    [SerializeField] public GameObject dialogue;
    private void OnTriggerEnter(Collider other)
    {
        dialogue.SetActive(true);
        //GetComponent<BoxCollider>().enabled=false;
    }
}
