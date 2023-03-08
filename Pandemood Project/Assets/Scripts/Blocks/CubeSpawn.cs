using System;
using System.Threading;
using UnityEngine;

namespace Blocks
{
    public class CubeSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject myPrefab;
        private GameObject _cube;
        [SerializeField] private PressurePlate plate;
        [SerializeField] private PressurePlate optionalPlate;

        [SerializeField] private GameObject particleEffect;
        [SerializeField] private AudioSource _spawnSound;
        private GameObject _particle;
        
        // Start is called before the first frame update
        void Start()
        {
            if (_cube == null)
            {
                _cube = Instantiate(myPrefab, transform.position, Quaternion.identity);
            }
            plate.ONPressure += SpawnCube;
            if (optionalPlate != null)
                optionalPlate.ONPressure += SpawnCube;
        }

        private void SpawnCube(object sender, EventArgs args)
        {
            if(_cube != null)
            {
                Transform _transform = _cube.transform;
                _particle = Instantiate(particleEffect, _transform.position, Quaternion.identity);
                if (_spawnSound != null)
                    _spawnSound.Play();
                _cube.transform.position = new Vector3(100000000.0f,10000000.0f,10000000.0f);
                Destroy(_cube, 0.2f);
                Destroy(_particle, 3f);
            }
            
            _cube = Instantiate(myPrefab, transform.position, Quaternion.identity);
        }
    }
}
