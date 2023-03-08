using UnityEngine;

namespace Blocks
{
    public class ShadowBlock : MonoBehaviour
    {
        private bool _player;
        private bool _plates;
        private int _triggered;
        private MeshRenderer _renderer;
        private bool _hasRenderer, _hasCollider;

        private BoxCollider _collider;

        // Start is called before the first frame update
        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<BoxCollider>();
            if (_renderer != null)
                _hasRenderer = true;
            if (_collider != null)
                _hasCollider = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_player || _plates)
            {
                if (_hasRenderer)
                        _renderer.enabled = false;
                if (_hasCollider)
                    _collider.isTrigger = true;
            }
            else
            {
                if (_hasRenderer)
                    _renderer.enabled = true;
                if (_triggered != 0) return;
                if (_hasCollider)
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

        public void ReleasePlate()
        {
            _plates = false;
        }

        public bool GetActive()
        {
            return 
                _player || _plates;
        }
    }
}