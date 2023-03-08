using Blocks;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterLight : MonoBehaviour
    {
        [SerializeField] private Light pointLight;
        [SerializeField] private AudioSource audioLight;
        [SerializeField] private GameObject _lightStamina;
        private bool _audio = false;
        private float _lightRadius;
        private ShadowBlock[] _shadowObjects;
        private charge staminaScript;

        // Start is called before the first frame update
        private void Start()
        {
            _shadowObjects = FindObjectsOfType<ShadowBlock>();
            _lightRadius = pointLight.range;
            _audio = audioLight != null;
            staminaScript = _lightStamina.GetComponent<charge>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Light") && staminaScript.currentStatus>0)
            {
                ToggleLight();
            }
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