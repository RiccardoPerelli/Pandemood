using System.Collections;
using System.Collections.Generic;
using EnigmaGioia.Garage;
using UnityEngine;

public class PedanaOpenDoor : MonoBehaviour
{
    public GameObject Door;

    void OnTriggerEnter()
    {
        Door.GetComponent<DoorGarageOpen>().AddOpen();
    }
}
