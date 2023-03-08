using System.Collections;
using Slowdown;
using UnityEngine;
using UnityEngine.UI;

namespace UI.powerUps
{
    public class Time : MonoBehaviour
    {
        public Image statusBar;
        public float currentStatus;
        private const float MAXStatus = 1;

        public float loss = 0.1f;

        private readonly WaitForSeconds _regenTick = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _useTick = new WaitForSeconds(0.1f);

        private Coroutine _regen;
        private Coroutine _use;

        private TimeScaling _characterScript;


        // Start is called before the first frame update
        private void Start()
        {
            currentStatus = MAXStatus;
            statusBar.fillAmount = MAXStatus;
            _characterScript = FindObjectOfType<TimeScaling>();
        }

        // Update is called once per frame
        private void Update()
        {
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
                    _characterScript.StopSlowDown();
                }
            } else if (currentStatus <= 0 && _characterScript.GetScaling()<0.9f)
            {
                _characterScript.StopSlowDown();
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
