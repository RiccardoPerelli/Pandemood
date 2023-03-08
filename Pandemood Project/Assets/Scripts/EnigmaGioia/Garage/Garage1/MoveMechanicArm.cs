using System;
using Character.Character_Controller;
using UnityEngine;

namespace EnigmaGioia.Garage.Garage1
{
    public class MoveMechanicArm : MonoBehaviour
    {
        public GameObject MechanicArm;
        public Transform TargetPoint;
        public float speed = 2f;

        public AudioSource AudioArmMachine;

        private bool move = false;
        private Vector3 startPosition;

        // Start is called before the first frame update
        void Start()
        {
            startPosition = MechanicArm.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (move)
            {
                MechanicArm.transform.position = Vector3.MoveTowards(MechanicArm.transform.position, TargetPoint.position, speed * Time.deltaTime);
                if (Vector3.Distance(MechanicArm.transform.position, TargetPoint.position) < 0.01f)
                {
                    if (AudioArmMachine != null)
                        AudioArmMachine.Stop();
                }
            }
            else
            {
                MechanicArm.transform.position = Vector3.MoveTowards(MechanicArm.transform.position, startPosition, speed * Time.deltaTime);
                if (Vector3.Distance(MechanicArm.transform.position, startPosition) < 0.01f)
                {
                    if (AudioArmMachine != null)
                        AudioArmMachine.Stop();
                }
            }

        }

        private void StartMove(object sender, EventArgs args)
        {
            Debug.Log("Move");
            move = true;
            if (AudioArmMachine != null)
                AudioArmMachine.Play();
        }
        
        
        private void EndMove(object sender, EventArgs args)
        {
            Debug.Log("Stop Move");
            move = false;
            if (AudioArmMachine != null)
                AudioArmMachine.Play();
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
                if(move) EndMove(this, EventArgs.Empty);
            }
        }
    }
}
