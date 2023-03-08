using System;
using Character.Character_Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnigmaGioia.Garage.Garage2
{
    public class MoveCarToDestination : MonoBehaviour
    {
        [FormerlySerializedAs("MiniCar")] public GameObject miniCar;
        [FormerlySerializedAs("TargetPoint")] public Transform targetPoint;
        public float speed = 2f;

        [FormerlySerializedAs("AudioMiniCar")] public AudioSource audioMiniCar;
        private bool _move = false;

        // Update is called once per frame
        private void Update()
        {
            if (_move && Vector3.Distance(miniCar.transform.position, targetPoint.position) > 0.01f)
                miniCar.transform.position = Vector3.MoveTowards(miniCar.transform.position, targetPoint.position, speed * Time.deltaTime);
            else
            if (audioMiniCar != null)
                audioMiniCar.Stop();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure += StartMove;
                other.GetComponent<CharacterInteract>().ONRelease += EndMove;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterInteract>().ONPressure -= StartMove;
                other.GetComponent<CharacterInteract>().ONRelease -= EndMove;
            }
            if(_move) EndMove(this, EventArgs.Empty);
        }
        private void StartMove(object sender, EventArgs eventArgs)
        {
            _move = true;
            if (audioMiniCar != null)
                audioMiniCar.Play();
        }

        private void EndMove(object sender, EventArgs eventArgs)
        {
            _move = false;
            if (audioMiniCar != null)
                audioMiniCar.Stop();
        }
    }
}
