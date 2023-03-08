using System.Collections;
using System.Collections.Generic;
using EnigmaGioia.Garage;
using UnityEngine;

public class OpenTrashDoor : MonoBehaviour
{
    public GameObject TrashDoor;

    private bool open=false;
    private GetNearInteractableObjectAngry get_near;
    void Start()
    {
        get_near = GetComponent<GetNearInteractableObjectAngry>();
    }
    // Update is called once per frame
    void Update()
    {
        if (get_near.objectTaken)
        {
            get_near.objectTaken = false;
            TrashDoor.GetComponent<DoorGarageOpen>().AddOpen();
            Destroy(this);
        }
    }
}
