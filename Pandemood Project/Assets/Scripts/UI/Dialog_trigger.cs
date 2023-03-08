using Character.Character_Controller;
using UnityEngine;

namespace UI
{
    public class Dialog_trigger : MonoBehaviour
    {
        [SerializeField] public GameObject dialogue;
        [SerializeField] public PhysicsCharacterController player;
        [SerializeField] public GameObject testoTastiera;
        [SerializeField] public GameObject testoController;

        private void OnTriggerEnter(Collider other)
        {
            dialogue.SetActive(true);
            if (Input.GetJoystickNames().Length > 0) //se joystick
                testoTastiera.SetActive(false);
            else
               testoController.SetActive(false);
            GetComponent<BoxCollider>().enabled=false;
        }
    }
}
