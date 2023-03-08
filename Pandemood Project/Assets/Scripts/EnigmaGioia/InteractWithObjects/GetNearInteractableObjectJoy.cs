using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class GetNearInteractableObjectJoy : MonoBehaviour
{
    public bool kinematicAfterPosition = false;

    private GameObject ObjectPickUp;
    public bool objectTaken = false;

    public GameObject KeyboardInteraction;
    public GameObject ControllerInteraction;

    private bool _images = false;
    // Start is called before the first frame update
    void Start()
    {
        if (KeyboardInteraction != null && ControllerInteraction != null)
        {
            KeyboardInteraction.SetActive(false);
            _images = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
                    ObjectPickUp = other.gameObject;
                    //SHOW KEY IMAGE
                    if (_images && !objectTaken)
                        if (Input.GetJoystickNames().Length > 0) //se joystick
                            ControllerInteraction.SetActive(true);
                        else
                             KeyboardInteraction.SetActive(true);

                    //PUT OBJECT INTO STONE
                    if (!ObjectPickUp.GetComponent<InteractableObject>().pickedUp)
                    {
                        //HIDE KEY IMAGES
                        if (ObjectPickUp.GetComponent<InteractableObject>()._images)
                        {
                            ObjectPickUp.GetComponent<InteractableObject>()._images = false;
                            ObjectPickUp.GetComponent<InteractableObject>().KeyboardInteraction.SetActive(false);
                            ObjectPickUp.GetComponent<InteractableObject>().ControllerInteraction.SetActive(false);
                            ObjectPickUp.GetComponent<InteractableObject>().cantPickUp=true; //non puo più riprenderlo
                        }

                        if (_images)
                        {
                            KeyboardInteraction.SetActive(false);
                            ControllerInteraction.SetActive(false);
                            KeyboardInteraction = null;
                            ControllerInteraction = null;
                        }

                        //PUT OBJECT INTO STONE
                        ObjectPickUp.transform.position = transform.position;
                        ObjectPickUp.GetComponent<Rigidbody>().isKinematic = kinematicAfterPosition;
                        ObjectPickUp.transform.parent = transform;
                        objectTaken = true;
                    }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
                    //HIDE KEY IMAGE
                    if (_images && !objectTaken)
                    {
                        KeyboardInteraction.SetActive(false);
                        ControllerInteraction.SetActive(false);
                    }
        }
    }
}
