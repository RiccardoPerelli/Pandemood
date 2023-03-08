﻿using System;
using System.Collections;
using System.Collections.Generic;
using Character.Character_Controller;
using Slowdown;
using UnityEngine;
using UnityEngine.UI;

public class small : MonoBehaviour
{
    public Image statusBar;
    public float currentStatus;
    private float maxStatus = 1;

    public float loss = 0.1f;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private WaitForSeconds useTick = new WaitForSeconds(0.1f);

    private Coroutine regen;
    private Coroutine use;

    public static small instance;
    //private TimeScaling characterScript;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStatus = maxStatus;
        statusBar.fillAmount = maxStatus;
        //characterScript = FindObjectOfType<TimeScaling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Slowdown") && currentStatus > 0)
        {
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
