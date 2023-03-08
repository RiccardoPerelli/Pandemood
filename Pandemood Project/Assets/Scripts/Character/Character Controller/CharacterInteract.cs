using System;
using UnityEngine;

namespace Character.Character_Controller
{
    public class CharacterInteract : MonoBehaviour
    {
        public EventHandler ONPressure, ONRelease;

        // Start is called before the first frame update
        void Start()
        {
        }

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