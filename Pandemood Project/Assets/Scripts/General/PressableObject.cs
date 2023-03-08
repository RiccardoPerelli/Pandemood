using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableObject : MonoBehaviour
{
    public GameObject KeyboardInteraction;
    public GameObject ControllerInteraction;

    public bool _images = false;

    private bool playerNear = false;
    private bool active = false;

    void Start()
    {
        if (KeyboardInteraction != null && ControllerInteraction != null)
        {
            KeyboardInteraction.SetActive(false);
            ControllerInteraction.SetActive(false);
            _images = true;
        }
    }

    void Update()
    {
        //VISIBLE KEY INTERACTION
        if (playerNear)
        {
            if (_images)
                if (Input.GetJoystickNames().Length > 0) //se joystick
                    ControllerInteraction.SetActive(true);
                else
                    KeyboardInteraction.SetActive(true);
        }

        //HIDE KEY INTERACTION
        if (!playerNear)
        {
            if (_images)
            {
                KeyboardInteraction.SetActive(false);
                ControllerInteraction.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerNear = false;
        }
    }

}
