using System;
using System.Collections;
using Character.Character_Controller;
using DDSystem.Script;
using Doublsb.Dialog;
using Slowdown;
using UnityEngine;
using UnityEngine.UI;

namespace UI.powerUps
{
    public class time : MonoBehaviour
    {
        public Image statusBar;
        public float currentStatus;
        private const float MAXStatus = 1;

        public float loss = 0.1f;

        private readonly WaitForSeconds _regenTick = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _useTick = new WaitForSeconds(0.1f);

        private Coroutine _regen;
        private Coroutine _use;

        private VCharacterSlowdown _characterScript;

        private bool _dialogOpen;
        private DialogManager[] _dialogManagers;

        // Start is called before the first frame update
        private void Start()
        {
            currentStatus = MAXStatus;
            statusBar.fillAmount = MAXStatus;
            _characterScript = FindObjectOfType<VCharacterSlowdown>();
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
            if (Input.GetButtonDown("Slowdown") && currentStatus>0)
            {
                if (_characterScript.GetScaling()>0.9f)
                {
                    _use = StartCoroutine(UsePower());
                    _characterScript.StartSlowdown();
                }
                else
                {
                    _regen = StartCoroutine(RegenPower());
                    _characterScript.StopSlowdown();
                }
            } else if (currentStatus <= 0 && _characterScript.GetScaling()<0.9f)
            {
                _characterScript.StopSlowdown();
                _regen = StartCoroutine(RegenPower());
            }
        }

        private IEnumerator UsePower()
        {
            if (_regen != null)
            {
                StopCoroutine(_regen);
            }
            while (currentStatus > 0)
            {
                currentStatus -= MAXStatus / 100;
                statusBar.fillAmount = currentStatus;
                yield return _useTick;
            }
        }

        private IEnumerator RegenPower()
        {
            if (_use != null)
            {
                StopCoroutine(_use);
            }
            yield return new WaitForSeconds(2);
            while (currentStatus < MAXStatus)
            {
                currentStatus += MAXStatus / 100;
                statusBar.fillAmount = currentStatus;
                yield return _regenTick;
            }
        }
    }
}
