using System.Collections;
using System.Collections.Generic;
using Enigma_Rabbia;
using UnityEngine;

public class PressableObject : MonoBehaviour
{
    public GameObject KeyboardInteraction;
    public GameObject ControllerInteraction;

    public bool _images = false;

    private bool playerNear = false;
    private bool active = false;

    private AnimationCharacterController push = null;
    private Rimpicciolimento shrink = null;

    void Start()
    {
        if (KeyboardInteraction != null && ControllerInteraction != null)
        {
            KeyboardInteraction.SetActive(false);
            ControllerInteraction.SetActive(false);
            _images = true;
        }

        if(GameObject.FindGameObjectWithTag("PlayerAnimation")!=null)
            push = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<AnimationCharacterController>();
        if(GameObject.FindGameObjectWithTag("Player")!=null)
            shrink = GameObject.FindGameObjectWithTag("Player").GetComponent<Rimpicciolimento>();
    }

    void Update()
    {
        //VISIBLE KEY INTERACTION
        if (playerNear && !isShrinking() && !isPushing())
        {
            if (_images)
                if (Input.GetJoystickNames().Length > 0) //se joystick
                    ControllerInteraction.SetActive(true);
                else
                    KeyboardInteraction.SetActive(true);
        }

        //HIDE KEY INTERACTION
        if (!playerNear || isShrinking() || isPushing())
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


    bool isShrinking()
    {
        if (shrink != null)
            return shrink.shrink;
        return false;
    }

    bool isPushing()
    {
        if (push != null)
            return push._isPushing;
        return false;
    }

}
