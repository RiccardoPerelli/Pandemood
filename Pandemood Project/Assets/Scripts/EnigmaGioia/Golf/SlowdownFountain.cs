using Slowdown;
using UnityEngine;

namespace EnigmaGioia.Golf
{
    public class SlowdownFountain : MonoBehaviour
    {
        private TimeScaling _timeScaling;

        private ParticleSystem _particleSystem;
        // Start is called before the first frame update
        void Start()
        {
            _timeScaling = FindObjectOfType<TimeScaling>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            var particleSystemMain = _particleSystem.main;
            particleSystemMain.simulationSpeed = _timeScaling.GetScaling();
        }
    }
}
