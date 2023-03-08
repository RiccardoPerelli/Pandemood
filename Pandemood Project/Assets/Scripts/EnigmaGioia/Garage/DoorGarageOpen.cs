using Gamepad;
using UnityEngine;

namespace EnigmaGioia.Garage
{
    public class DoorGarageOpen : MonoBehaviour
    {
        public int NToOpen;
        public Transform TargetOpen;
        public float speedOpen = 5f;
        private bool open = false;
        private int n = 0;

        public AudioSource AudioCompleted;
        public AudioSource AudioAddOpen;
        private bool _vibrating;
        void Start()
        {
        }

        void Update()
        {
            if (open && Vector3.Distance(transform.position, TargetOpen.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetOpen.position, speedOpen * Time.deltaTime);
                if(!_vibrating)
                {
                    _vibrating = true;
                    MyGamepad.SetVibration(MyGamepad.DoorVibration, MyGamepad.DoorVibration);
                }
            }
            else if(_vibrating)
            {
                _vibrating = false;
                MyGamepad.StopVibration();
            }
        }

        public void AddOpen()
        {
            n++;
            if (n == NToOpen) //open
            {
                open = true;
                if (AudioCompleted != null)
                    AudioCompleted.Play();
            }

            if(n < NToOpen )
                if (AudioAddOpen != null)
                    AudioAddOpen.Play();
        }
    }
}
