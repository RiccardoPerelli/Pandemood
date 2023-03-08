using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class GetNearInteractableObjectAngry: MonoBehaviour
{
    private GameObject ObjectPickUp;
    public string typeTrash;
    public bool objectTaken = false;

    public GameObject KeyboardInteraction;
    public GameObject ControllerInteraction;

    private bool _images = false;
    // Start is called before the first frame update
    void Start()
    {
        if (KeyboardInteraction != null && ControllerInteraction != null)
        {
            ControllerInteraction.SetActive(false);
            KeyboardInteraction.SetActive(false);
            _images = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.GetComponent<TypeTrash>() != null)
                if (other.GetComponent<TypeTrash>().typeTrash == typeTrash)
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
                            ObjectPickUp.GetComponent<InteractableObject>().cantPickUp = true; //non puo più riprenderlo
                        }

                        if (_images)
                        {
                            KeyboardInteraction.SetActive(false);
                            ControllerInteraction.SetActive(false);
                            KeyboardInteraction = null;
                            ControllerInteraction = null;
                        }

                        // PUT OBJECT INTO STONE
                        ObjectPickUp.GetComponent<Rigidbody>().isKinematic = true;
                        ObjectPickUp.transform.parent = transform;
                        ObjectPickUp.transform.position = transform.position;
                        objectTaken = true;

                        Destroy(this);
                    }
                }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (other.GetComponent<TypeTrash>() != null)
                if (other.GetComponent<TypeTrash>().typeTrash == typeTrash)
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
}
