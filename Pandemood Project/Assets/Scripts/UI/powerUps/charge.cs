﻿using System;
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

    [SerializeField] private VCharacterLight characterScript;

    // Start is called before the first frame update
    void Start()
    {
        currentStatus = maxStatus;
        statusBar.fillAmount = maxStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Light") && currentStatus>0)
        {
            if (!characterScript.IsLightActive())
            {
                use = StartCoroutine(usePower());
            }
            else
            {
                regen = StartCoroutine(RegenPower());
            }
        } else if (currentStatus <= 0 && characterScript.IsLightActive())
        {
            characterScript.ToggleLight();
            regen = StartCoroutine(RegenPower());
        }
    }

    private IEnumerator usePower()
    {
        if (regen != null)
        {
            StopCoroutine(regen);
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
