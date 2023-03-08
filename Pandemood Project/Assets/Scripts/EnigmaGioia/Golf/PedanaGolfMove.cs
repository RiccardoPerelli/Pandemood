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

        private bool _arrived;
        [Header("Vertical movement at the end")]
        [SerializeField] private bool upAtEnd;
        [SerializeField] private float yTranslation = 1;
        [SerializeField] private float verticalSpeed = 5;
        private Vector3 _upDestination;
        // Start is called before the first frame update
        void Start()
        {
            _upDestination = Point.transform.position;
            _upDestination.y += yTranslation;
        }

        // Update is called once per frame
        void Update()
        {
            if (clicked && !(upAtEnd && _arrived))
            {
                MachineGolf.transform.position = Vector3.MoveTowards(MachineGolf.transform.position, Point.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(MachineGolf.transform.position, Point.transform.position) < 0.01f)
                {
                    if (AudioArmMachine != null && AudioArmMachine.isPlaying)
                        AudioArmMachine.Stop();
                    _arrived = true;
                }
            }
            else if (upAtEnd && _arrived)
            {
                MachineGolf.transform.position = Vector3.MoveTowards(MachineGolf.transform.position, _upDestination, verticalSpeed * Time.deltaTime);
            }
        }
    
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
