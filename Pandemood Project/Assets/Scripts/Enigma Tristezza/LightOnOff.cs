<<<<<<< .merge_file_a19692
﻿using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class LightOnOff : MonoBehaviour
    {
        [FormerlySerializedAs("DistanceIllumination")]
        public float distanceIllumination = 5f;

        public bool lightOn;
        [FormerlySerializedAs("AudioLightOn")] public AudioSource audioLightOn;

        private GameObject _illuminationTexture;
        private Renderer _renderer, _illuminationRenderer;

        // Start is called before the first frame update
        private void Start()
        {
            transform.localScale = new Vector3(distanceIllumination, distanceIllumination, distanceIllumination);
            _renderer = GetComponent<Renderer>();
            _renderer.enabled = false;
            _illuminationTexture = transform.GetChild(0).gameObject;
            _illuminationRenderer = _illuminationTexture.GetComponent<Renderer>();
            _illuminationRenderer.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (lightOn) //spegni
            {
                lightOn = false;
                _renderer.enabled = false;
                _illuminationRenderer.enabled = false;
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public float DistanceIllumination=5f;
    public bool lightOn = false;
    public AudioSource AudioLightOn;

    private GameObject illuminationTexture;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(DistanceIllumination, DistanceIllumination, DistanceIllumination);
        GetComponent<Renderer>().enabled = false;
        illuminationTexture = transform.GetChild(0).gameObject;
        illuminationTexture.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (lightOn) //spegni
            {
                lightOn = false;
                GetComponent<Renderer>().enabled = false;
                illuminationTexture.GetComponent<Renderer>().enabled = false;
>>>>>>> .merge_file_a16848
            }
            else //accendi
            {
                lightOn = true;
<<<<<<< .merge_file_a19692
                _renderer.enabled = true;
                _illuminationRenderer.enabled = true;
                audioLightOn.Play();
            }
        }
    }
}
=======
                GetComponent<Renderer>().enabled = true;
                illuminationTexture.GetComponent<Renderer>().enabled = true;
                AudioLightOn.Play();
            }
    }
}
>>>>>>> .merge_file_a16848
