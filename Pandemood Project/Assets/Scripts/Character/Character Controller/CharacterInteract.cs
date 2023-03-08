using System;
using Enigma_Rabbia;
using UnityEngine;

namespace Character.Character_Controller
{
    public class CharacterInteract : MonoBehaviour
    {
        public EventHandler ONPressure, ONRelease;
        private AnimationCharacterController push = null;
        private Rimpicciolimento shrink = null;
        void Start()
        {
            if (GameObject.FindGameObjectWithTag("PlayerAnimation") != null)
                push = GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<AnimationCharacterController>();
            if (GameObject.FindGameObjectWithTag("Player") != null)
                shrink = GameObject.FindGameObjectWithTag("Player").GetComponent<Rimpicciolimento>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Interact") && !isPushing() && !isShrinking())
            {
                ONPressure?.Invoke(this, EventArgs.Empty);
            }

            if (Input.GetButtonUp("Interact") && !isPushing() && !isShrinking())
            {
                ONRelease?.Invoke(this, EventArgs.Empty);
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
}