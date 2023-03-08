using System;
using Slowdown;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterSlowdown : MonoBehaviour
    {
        [SerializeField] private TimeScaling timeScaling;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Slowdown"))
            {
                timeScaling.StartSlowdown();
            }
        }
    }
}