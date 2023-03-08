using Blocks;
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

        // Start is called before the first frame update
        private void Awake()
        {
            _shadowObjects = FindObjectsOfType<ShadowBlock>();
            
            _audio = audioLight != null;
            _staminaScript = lightStamina.GetComponent<charge>();
        }

        // Update is called once per frame
        private void Update()
        {
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
        }


        public bool IsLightActive()
        {
            return pointLight.intensity > 0.1f;
        }
    }
}