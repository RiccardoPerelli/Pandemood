using UnityEngine;

namespace Slowdown
{
    public class TimeScaling : MonoBehaviour
    {
        [SerializeField] private float duration = 2;
        [SerializeField] private float scaling = 0.5f;
        private float _time;
        [SerializeField] private AudioSource audioRallentyStart;
        [SerializeField] private AudioSource audioRallentyAmbience;


        private void Update()
        {
            if (_time <= 0) return;
            _time -= Time.deltaTime;
            
            if (_time > 0) return;
            
            if (audioRallentyStart != null)
                audioRallentyStart.Stop();

            if (audioRallentyAmbience != null)
                audioRallentyAmbience.Stop();
        }

        public void StartSlowdown()
        {
<<<<<<< .merge_file_a21816
            if (_time <= 0)
                _time = duration;
=======
            if (_time > 0) return;
            if (audioRallentyStart != null)
                audioRallentyStart.Play();
            if (audioRallentyAmbience != null)
                audioRallentyAmbience.Play();
            _time = duration;
>>>>>>> .merge_file_a04416
        }

        public float GetScaling()
        {
            if (_time > 0)
            {
                return scaling;
            }

            return 1;
        }
    }
}