using System;
using UnityEngine;

namespace Blocks
{
    public class ShadowBlock : MonoBehaviour
    {
        private bool _player;
        private bool _plates;
        private int _triggered;
        private MeshRenderer _renderer;

        private BoxCollider _collider;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_player || _plates)
            {
                GetComponent<ParticleSystemRenderer>().enabled = false;
                _renderer.enabled = false;
                _collider.isTrigger = true;
            }
            else
            {
                GetComponent<ParticleSystemRenderer>().enabled = true;
                _renderer.enabled = true;
                if (_triggered == 0)
                    _collider.isTrigger = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            _triggered++;
        }

        private void OnTriggerExit(Collider other)
        {
            _triggered--;
        }

        public void SetPlayer(bool val)
        {
            _player = val;
        }

        public void PressPlate()
        {
            _plates = true;
        }

        public void UnpressPlate()
        {
            _plates = false;
        }
    }
}