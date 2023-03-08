using System;
using Slowdown;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterSlowdown : MonoBehaviour
    {
        private TimeScaling _timeScaling;

        private bool _hasTimeScaling;

        private void Start()
        {
            _timeScaling = FindObjectOfType<TimeScaling>();
            _hasTimeScaling = _timeScaling != null;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Slowdown") && _hasTimeScaling)
            {
                _timeScaling.StartSlowdown();
            }
        }
    }
}