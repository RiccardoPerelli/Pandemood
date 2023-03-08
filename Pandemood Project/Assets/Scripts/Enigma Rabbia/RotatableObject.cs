using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public GameObject KeyboardInteraction;
    public GameObject ControllerInteraction;
    public Material GreenMat;
    public Material RedMat;

    public GameObject Engine;
    private PropellerEngine engine;

    public bool _images = false;

    private bool playerNear = false;
    private bool active=false;

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

        engine = Engine.GetComponent<PropellerEngine>();

        if (GameObject.FindGameObjectWithTag("PlayerAnimation") != null)
            push = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<AnimationCharacterController>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
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

            if (Input.GetButtonDown("Interact")) //COMMAND TO ROTATE
                if (!active)
                {
                    //ROTATE
                    transform.localEulerAngles = new Vector3(30f, 0, -90f);
                    transform.GetChild(0).gameObject.GetComponent<Renderer>().material = GreenMat;
                    engine.AddOpen();
                    active = true;
                }
                else
                {
                    //RETURN BACK
                    transform.localEulerAngles = new Vector3(0f, 0, -90f);
                    transform.GetChild(0).gameObject.GetComponent<Renderer>().material = RedMat;
                    engine.SubstractOpen();
                    active = false;
                }

        }

        //HIDE KEY INTERACTION
        if (!playerNear ||isPushing() || isShrinking())
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
