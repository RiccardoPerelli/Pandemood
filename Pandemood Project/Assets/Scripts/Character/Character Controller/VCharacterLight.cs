using System;
using Blocks;
using DDSystem.Script;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Character_Controller
{
    public class VCharacterLight : MonoBehaviour
    {
        [SerializeField] private Light pointLight;
        [SerializeField] private AudioSource audioLight;
        [FormerlySerializedAs("_lightStamina")] [SerializeField] private GameObject lightStamina;
        private bool _audio;
        [SerializeField] private float lightRadius =4;
        private ShadowBlock[] _shadowObjects;
        private charge _staminaScript;
        
        private bool _dialogOpen;
        private DialogManager[] _dialogManagers;

        [SerializeField] private GameObject particleEffect;
        [SerializeField] private GameObject particlePosition;
        private GameObject _particle;

        // Start is called before the first frame update
        private void Awake()
        {
            _shadowObjects = FindObjectsOfType<ShadowBlock>();
            
            _audio = audioLight != null;
            _staminaScript = lightStamina.GetComponent<charge>();
            
            _dialogManagers = Resources.FindObjectsOfTypeAll<DialogManager>();
            foreach (var dialog in _dialogManagers)
            {
                dialog.OnActivate += OnDialogOpen;
                dialog.OnDeactivate += OnDialogClose;
            }
        }


        void OnDialogOpen(object o, EventArgs e)
        {
            _dialogOpen = true;
        }
        
        
        void OnDialogClose(object o, EventArgs e)
        {
            _dialogOpen = false;
        }


        // Update is called once per frame
        private void Update()
        {
            if (inGameMenu.GameIsPaused) return;
            if (_dialogOpen) return;
            if(_staminaScript == null) return;
            if (Input.GetButtonDown("Light") && _staminaScript.currentStatus>0)
            {
                ToggleLight();
            }
            if (pointLight.intensity != 0)
                foreach (var shadow in _shadowObjects)
                {
                    if(shadow != null)
                        shadow.SetPlayer(Vector3.Distance(transform.position, shadow.transform.position) <= lightRadius);
                }
            else
                foreach (var shadow in _shadowObjects)
                {
                    shadow.SetPlayer(false);
                }
        }


        public void ToggleLight()
        {
            pointLight.intensity = pointLight.intensity == 0 ? 2 : 0;
            if (_audio)
                audioLight.Play();
            if (_particle)
            {
                Destroy(_particle);
            }
            else
            {
                _particle = Instantiate(particleEffect, particlePosition.transform.position, Quaternion.identity);
                _particle.transform.parent = particlePosition.transform;
            }
        }


        public bool IsLightActive()
        {
            return pointLight.intensity > 0.1f;
        }
    }
}