<<<<<<< .merge_file_a25468
﻿using Blocks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class LightObject : MonoBehaviour
    {
        [FormerlySerializedAs("Illumination")] public GameObject illumination;
        public GameObject particles;
        [FormerlySerializedAs("AudioShadow")] public AudioSource audioShadow;
        [FormerlySerializedAs("DistanceIllumination")] public float distanceIllumination = 5f;
        private GameObject _player;
        private Renderer _renderer;
        private Collider _collider;
        private LightOnOff _lightOnOff;
        private ShadowBlock _shadowBlock;
        public bool illuminated;
        private bool _hasRenderer, _hasParticles;

        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _renderer = GetComponent<Renderer>();
            _collider = GetComponent <Collider>();
            _lightOnOff = illumination.GetComponent<LightOnOff>();
            _shadowBlock = GetComponent<ShadowBlock>();
            _hasRenderer = _renderer != null;
            _hasParticles = particles != null;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_shadowBlock == null) return;
            if (_shadowBlock.GetActive()) //SCOMPARE OGGETTO, LUCE ACCESA
            {
                _collider.enabled = false;
                if(_hasRenderer)
                    _renderer.enabled = false;
                if(_hasParticles)
                    particles.SetActive(false);
                if (!illuminated)
                    audioShadow.Play();

                illuminated = true;
            }
            else //COMPARE OGGETTO, LUCE SPENTA
            {
                _collider.enabled = true;
                if (_hasRenderer)
                    _renderer.enabled = true;
                if (_hasParticles)
                    particles.SetActive(true);

                if (illuminated)
                    audioShadow.Play();
                illuminated = false;
            }
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public GameObject Illumination;
    public GameObject particles;
    public AudioSource AudioShadow;
    public float DistanceIllumination = 5f;
    private GameObject Player;

    public bool illuminated=false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance < DistanceIllumination && Illumination.GetComponent<LightOnOff>().lightOn) //SCOMPARE OGGETTO, LUCE ACCESA
        {
            GetComponent<Collider>().enabled = false;
            if(GetComponent<Renderer>()!=null)
                GetComponent<Renderer>().enabled = false;
            if(particles!=null)
                particles.SetActive(false);
            if (!illuminated)
                AudioShadow.Play();

             illuminated = true;
        }
        else //COMPARE OGGETTO, LUCE SPENTA
        {
            GetComponent<Collider>().enabled = true;
            if (GetComponent<Renderer>() != null)
                GetComponent<Renderer>().enabled = true;
            if (particles != null)
                particles.SetActive(true);

            if (illuminated)
                AudioShadow.Play();
            illuminated = false;
>>>>>>> .merge_file_a14624
        }
    }
}
