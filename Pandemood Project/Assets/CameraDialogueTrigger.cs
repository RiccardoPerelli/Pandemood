using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Character_Controller;
public class CameraDialogueTrigger : MonoBehaviour
{
    [SerializeField] public GameObject dialogue;
    [SerializeField] public GameObject testoTastiera;
    [SerializeField] public GameObject testoController;
    [SerializeField] public PhysicsCharacterController player;
    [SerializeField] public AnimationCharacterController aCC;

    private void DisableAll()
    {
        player.enabled = false;
        aCC.enabled = false;
    }

    void ActivateDialogue()
    {
        if (Input.GetJoystickNames().Length > 0) //se joystick
            testoTastiera.SetActive(false);
        else
            testoController.SetActive(false);
        player.enabled = true;
        aCC.enabled = true;
        dialogue.SetActive(true);
    }
}
