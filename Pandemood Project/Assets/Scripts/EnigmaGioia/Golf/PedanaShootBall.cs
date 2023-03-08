using System;
using Character.Character_Controller;
using UnityEngine;

namespace EnigmaGioia.Golf
{
    public class PedanaShootBall : MonoBehaviour
    {
        private GameObject _clone;
        public GameObject Ball;
        public GameObject StartPositionShot;
        public float impulseForce = 50f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure += Shot;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure -= Shot;
            }
        }
        
        private void Shot(object sender, EventArgs eventArgs)
        {
            if (_clone != null) Destroy(_clone);
            _clone = Instantiate(Ball, StartPositionShot.transform.position,
                StartPositionShot.transform.rotation);
            _clone.GetComponent<Rigidbody>().isKinematic = false;
            _clone.GetComponent<Rigidbody>().AddForce(StartPositionShot.transform.forward * impulseForce);
        }
    }
}