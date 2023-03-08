using Blocks;
using UnityEngine;

namespace Character.Character_Controller
{
    public class VCharacterLight : MonoBehaviour
    {
        [SerializeField] Light pointLight;
         private float _lightRadius;
        private ShadowBlock[] _shadowObjects;
        [SerializeField] private float _intensity = 5f;
            
        // Start is called before the first frame update
        void Start()
        {
            _shadowObjects = FindObjectsOfType<ShadowBlock>();
            _lightRadius = pointLight.range;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1")) ToggleLight();
            if (pointLight.intensity != 0)
                foreach (var shadow in _shadowObjects)
                {
                    if(Vector3.Distance(transform.position, shadow.transform.position) <= _lightRadius)
                        shadow.SetPlayer(true);
                    else
                        shadow.SetPlayer(false);
                }
            else
                foreach (var shadow in _shadowObjects)
                {
                    shadow.SetPlayer(false);
                }
        }


        void ToggleLight()
        {
            pointLight.intensity = pointLight.intensity == 0 ? _intensity : 0;
        }


        public bool IsLightActive()
        {
            return pointLight.intensity != 0;
        }
    }
}