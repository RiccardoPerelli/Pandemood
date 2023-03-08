﻿using Blocks;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterLight : MonoBehaviour
    {
        [SerializeField] private Light pointLight;
         private float _lightRadius;
        private ShadowBlock[] _shadowObjects;
        
            
        // Start is called before the first frame update
        private void Start()
        {
            _shadowObjects = FindObjectsOfType<ShadowBlock>();
            _lightRadius = pointLight.range;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Light")) ToggleLight();
            if (pointLight.intensity != 0)
                foreach (var shadow in _shadowObjects)
                {
                    if(shadow != null)
                        shadow.SetPlayer(Vector3.Distance(transform.position, shadow.transform.position) <= _lightRadius);
                }
            else
                foreach (var shadow in _shadowObjects)
                {
                    shadow.SetPlayer(false);
                }
        }


        void ToggleLight()
        {
            pointLight.intensity = pointLight.intensity == 0 ? 2 : 0;
        }


        public bool IsLightActive()
        {
            return pointLight.intensity != 0;
        }
    }
}