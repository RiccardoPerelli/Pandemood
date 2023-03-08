using System;
using Character.Character_Controller;
using UnityEngine;

namespace EnigmaGioia.Golf
{
    public class PedanaRotate : MonoBehaviour
    {
        public GameObject PareteGolf;
        private Quaternion _angle;

        private void Awake()
        {
            _angle = PareteGolf.transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure += Rotate;
                other.GetComponent<CharacterInteract>().ONRelease += (sender, args) => Reset();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure -= Rotate;
                other.GetComponent<CharacterInteract>().ONRelease -= (sender, args) => Reset();
            }

            Reset();
        }

        private void Rotate(object sender, EventArgs args)
        {
            PareteGolf.transform.rotation = _angle * Quaternion.Euler(0, 0, 90);
        }

        private void Reset()
        {
            PareteGolf.transform.rotation = _angle;
        }
    }
}