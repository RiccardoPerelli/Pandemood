using System;
using DDSystem.Script;
using UnityEngine;

namespace Enigma_Rabbia
{
    public class Rimpicciolimento : MonoBehaviour
    {
        public delegate void Small();
        public static event Small SmallTriggered;
        public delegate void Big();
        public static event Big BigTriggered;

        public bool shrink;

        public GameObject particleEffect;
        private small _staminaScript;

        private CapsuleCollider _collider;
        [SerializeField] private AudioSource audioSmall;
        private bool _audio;


        private bool _dialogOpen;
        private DialogManager[] _dialogManagers;
        
        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
            _audio = audioSmall != null;
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
            if(Input.GetButtonDown("Shrink") && !inGameMenu.GameIsPaused && !_dialogOpen)
            {
                if (!shrink)
                {
                    smaller();
                }
                else
                {
                    bigger();
                }
                if (_audio && audioSmall != null)
                {
                    audioSmall.Play();
                }
            }
        }

        public void smaller()
        {
            var transform1 = transform;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            SmallTriggered?.Invoke();
            shrink = true;
            GameObject tmp = Instantiate(particleEffect, transform1.position, transform1.rotation);
            tmp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        public void bigger()
        {
            var transform1 = transform;
            var position = transform1.position;
            Vector3 start = position, end = position;
            start.y += _collider.radius + 0.01f;
            end.y += _collider.height;
            var col = new Collider[1];
            if(Physics.OverlapCapsuleNonAlloc(start, end, _collider.radius, col, ~LayerMask.GetMask("Player")) != 0) return;
            transform.localScale = new Vector3(1f, 1f, 1f);
            BigTriggered?.Invoke();
            shrink = false;
            Instantiate(particleEffect, position, transform1.rotation);
        }

        public bool IsShrink()
        {
            return shrink;
        }
    }
}
