using System;
using UnityEngine;

namespace Blocks
{
    public class PressurePlate : MonoBehaviour
    {
        private int _nOfObject;
        [SerializeField] private GameObject light;
        [SerializeField] private Material turnOn;
        [SerializeField] private Material turnOff;

        private Color on;
        private Color off;
        private bool hasColor;
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        public event EventHandler ONPressure, ONRelease;

        private void Start()
        {
            hasColor = turnOn != null && turnOff != null;
            if (hasColor)
            {
                on = turnOn.GetColor(Color1);
                off = turnOff.GetColor(Color1);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _nOfObject++;
            if (_nOfObject <= 0) return;
            ONPressure?.Invoke(this, EventArgs.Empty);
            if(hasColor)
                light.GetComponent<MeshRenderer>().materials[3].SetColor(Color1, off);
        }

        private void OnTriggerExit(Collider other)
        {
            _nOfObject--;
            if (_nOfObject > 0) return;
            ONRelease?.Invoke(this, EventArgs.Empty);
            if (hasColor)
                light.GetComponent<MeshRenderer>().materials[3].SetColor(Color1, on);
        }
    }
}