using System;
using System.Collections;
using System.Collections.Generic;
using Character.Character_Controller;
using UnityEngine;
using UnityEngine.UI;

public class charge : MonoBehaviour
{
    public Image statusBar;
    public float currentStatus;
    private float maxStatus = 1;

    public float loss = 0.1f;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private WaitForSeconds useTick = new WaitForSeconds(0.1f);

    private Coroutine regen;
    private Coroutine use;

    private bool _isUsing, _isRegen;

    [SerializeField] private VCharacterLight characterScript;

    // Start is called before the first frame update
    void Start()
    {
        currentStatus = maxStatus;
        statusBar.fillAmount = maxStatus;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Light") && currentStatus > 0)
        {
            if (characterScript.IsLightActive())
            {
                if (_isUsing) return;
                use = StartCoroutine(usePower());
                _isUsing = true;
            }
            else
            {
                if (_isRegen) return;
                regen = StartCoroutine(RegenPower());
                _isRegen = true;
            }
        }
        else if (currentStatus <= 0.0001)
        {
            if (characterScript.IsLightActive())
                characterScript.ToggleLight();
            if (!_isRegen)
            {
                regen = StartCoroutine(RegenPower());
                _isRegen = true;
            }
        }
    }

    private IEnumerator usePower()
    {
        if (_isRegen)
        {
            StopCoroutine(regen);
            _isRegen = false;
        }

        while (currentStatus > 0)
        {
            currentStatus -= maxStatus / 100;
            statusBar.fillAmount = currentStatus;
            yield return useTick;
        }
    }

    private IEnumerator RegenPower()
    {
        if (use != null)
        {
            StopCoroutine(use);
            _isUsing = false;
        }

        yield return new WaitForSeconds(2);
        while (currentStatus < maxStatus)
        {
            currentStatus += maxStatus / 100;
            statusBar.fillAmount = currentStatus;
            yield return regenTick;
        }
    }
}