using System;
using UnityEngine;

namespace Slowdown
{
    public class TimeScaling : MonoBehaviour
    {
        [SerializeField] private float duration = 2;
        [SerializeField] private float scaling = 0.5f;
        private bool _time;

        public void StartSlowdown()
        {
            _time = true;
        }

        public void StopSlowDown()
        {
            _time = false;
        }
        
        

        public float GetScaling()
        {
            if (_time)
            {
                return scaling;
            }

            return 1;
        }
    }
}