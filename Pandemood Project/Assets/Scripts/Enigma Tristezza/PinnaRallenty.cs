﻿using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class PinnaRallenty : MonoBehaviour
    {
        [FormerlySerializedAs("FishAnimator")] public GameObject fishAnimator;
        [FormerlySerializedAs("ShadowPinna")] public GameObject shadowPinna;
        [FormerlySerializedAs("Shape")] public GameObject shape;
        [FormerlySerializedAs("TransparentGreen")] public Material transparentGreen;
        public float distancePut = 0.1f;
        private Renderer _renderer;
        private RallentyTime _rallentyTime;
        private LightObject _light;
<<<<<<< .merge_file_a03260
        private bool _hasShape;
        private Shape _shape;
        // Start is called before the first frame update
        private void Start()
        {
            _rallentyTime = fishAnimator.GetComponent<RallentyTime>();
            _shape = shape.GetComponent<Shape>();
            _hasShape = shape != null;
            _renderer = GetComponent<Renderer>();
            _light = shadowPinna.GetComponent<LightObject>();
            _hasShape = shape != null;
        }

        // Update is called once per frame
        private void Update()
=======

        private Shape _shape;
        // Start is called before the first frame update
        void Start()
        {
            _rallentyTime = fishAnimator.GetComponent<RallentyTime>();
            _shape = shape.GetComponent<Shape>();
            _renderer = GetComponent<Renderer>();
            _light = shadowPinna.GetComponent<LightObject>();
        }

        // Update is called once per frame
        void Update()
>>>>>>> .merge_file_a06544
        {
            shadowPinna.SetActive(_rallentyTime.rallenty);

            if (!_rallentyTime.rallenty || !_light.illuminated) return;
            var distance = Vector3.Distance(transform.position, shape.transform.position);
            if (distance >= distancePut) return;
            _renderer.material = transparentGreen; //change material
<<<<<<< .merge_file_a03260
            if (_hasShape)
=======
            if (_shape != null)
>>>>>>> .merge_file_a06544
                _shape.ShapeInsert(); //shape insert correctly
            var transform1 = transform;
            transform1.position = shape.transform.position;
            transform1.parent = shape.transform;

            Destroy(shadowPinna);
            Destroy(this);
        }
    }
}
