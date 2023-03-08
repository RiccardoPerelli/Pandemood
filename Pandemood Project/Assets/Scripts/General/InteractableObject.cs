using Enigma_Rabbia;
using UnityEngine;

namespace General
{
    public class InteractableObject : MonoBehaviour
    {
        public bool pickedUp = false;
        public bool cantPickUp = false;
        public float distanceInteractionKeyImage = 2f;
        public GameObject KeyboardInteraction;
        public GameObject ControllerInteraction;
        public bool _images=false;


        private Renderer keyboard;
        private Renderer controller;
        private bool playerNear;
        GameObject Player;

        private AnimationCharacterController push=null;
        private Rimpicciolimento shrink=null;

        private void Start()
        {
            if (KeyboardInteraction != null && ControllerInteraction!=null) {
                keyboard = KeyboardInteraction.GetComponent<Renderer>();
                controller = ControllerInteraction.GetComponent<Renderer>();
                keyboard.enabled = false;
                controller.enabled = false;
                _images = true;
            }

            Player = GameObject.FindGameObjectWithTag("Player");
            if (GameObject.FindGameObjectWithTag("PlayerAnimation")!=null)
                push = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<AnimationCharacterController>();
            if(GameObject.FindGameObjectWithTag("Player")!=null)
                shrink = GameObject.FindGameObjectWithTag("Player").GetComponent<Rimpicciolimento>();
        }

        private void Update()
        {
            //VISIBLE KEY INTERACTION
            if(!pickedUp && playerNear && !isShrinking() && !isPushing())
            {
                if (_images)
                    if(Input.GetJoystickNames().Length > 0) //se joystick
                        controller.enabled = true;
                    else
                        keyboard.enabled = true;
            }

            //HIDE KEY INTERACTION
            if (pickedUp || !playerNear || isShrinking() || isPushing())
            {
                if (_images)
                {
                    keyboard.enabled = false;
                    controller.enabled = false;
                }
            }
        }

        private void FixedUpdate()
        {
            //--------- CONSIDER THE CLOSEST OBJECT CAN PICK UP ----------
            if (Player != null)
                ControlDistanceFromPlayer();
        }


        //-----------SET CLOSEST OBJECT CAN PICK UP--------------
        private void ControlDistanceFromPlayer()
        {
            var diff =Vector3.Distance(Player.transform.position,transform.position);
            playerNear = diff < distanceInteractionKeyImage;
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
}
