using System;
using Slowdown;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterSlowdown : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSlow;
        public TimeScaling _timeScaling;

        public bool _hasTimeScaling;
        private bool _audio;
        
        [SerializeField] private GameObject particleEffect;
        private GameObject _particle;

        private void Start()
        {
            _timeScaling = FindObjectOfType<TimeScaling>();
            _hasTimeScaling = _timeScaling != null;
            _audio = audioSlow != null;
        }

        // Update is called once per frame
        public void StartSlowdown()
        {
            if(!_hasTimeScaling) return;
            _timeScaling.StartSlowdown();
            if (_audio)
                audioSlow.Play();
            
            _particle = Instantiate(particleEffect, transform.position, Quaternion.identity);
            _particle.transform.parent = transform;
        }

        public float GetScaling()
        {
            return _timeScaling.GetScaling();
        }
        
        public void StopSlowdown()
        {
            if(!_hasTimeScaling) return;
            _timeScaling.StopSlowDown();
            if (_audio)
                audioSlow.Stop();
            if(_particle)
                Destroy(_particle);
        }
        
        
    }
}