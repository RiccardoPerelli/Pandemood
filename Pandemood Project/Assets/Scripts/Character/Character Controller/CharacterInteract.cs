using System;
using UnityEngine;

namespace Character.Character_Controller
{
    public class CharacterInteract : MonoBehaviour
    {
        public EventHandler ONPressure, ONRelease;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                ONPressure?.Invoke(this, EventArgs.Empty);
            }

            if (Input.GetButtonUp("Interact"))
            {
                ONRelease?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}