using System;
using Character.Character_Controller;
using UnityEngine;

namespace EnigmaGioia.Golf
{
    public class PedanaGolfMove : MonoBehaviour
    {
        public GameObject MachineGolf;
        public bool clicked = false;

        public GameObject Point;

        public AudioSource AudioArmMachine;

        public float speed = 5f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (clicked)
            {
                MachineGolf.transform.position = Vector3.MoveTowards(MachineGolf.transform.position, Point.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(MachineGolf.transform.position, Point.transform.position) < 0.01f)
                {
                    if (AudioArmMachine != null)
                        AudioArmMachine.Stop();
                }
            }
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         clicked = true;
        //         if (AudioArmMachine != null)
        //             AudioArmMachine.Play();
        //     }
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         clicked = false;
        //         if (AudioArmMachine != null)
        //             AudioArmMachine.Stop();
        //     }
        // }
    
        private void StartMove(object sender, EventArgs eventArgs)
        {
            clicked = true;
            if (AudioArmMachine != null)
                AudioArmMachine.Play();
        }

        private void EndMove(object sender, EventArgs eventArgs)
        {
            clicked = false;
            if (AudioArmMachine != null)
                AudioArmMachine.Stop();
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
            if(clicked) EndMove(this, EventArgs.Empty);
        }
    }
}
