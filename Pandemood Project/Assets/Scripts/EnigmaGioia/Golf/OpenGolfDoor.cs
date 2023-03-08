﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGolfDoor : MonoBehaviour
{
    public int NBallsToOpen;

    private bool open = false;
    private int nballs = 0;

    public AudioSource AudioCompleted;
    public AudioSource AudioAddOpen;

    void Start()
    {
    }

    public void AddBall()
    {
        nballs++;
        if (nballs == NBallsToOpen) //open
        {
            transform.parent.transform.rotation = Quaternion.Euler(transform.parent.transform.eulerAngles.x, transform.parent.transform.eulerAngles.y - 90f, transform.parent.transform.eulerAngles.z);
            if (AudioCompleted != null)
                AudioCompleted.Play();
        }

        if(nballs< NBallsToOpen) 
        {
            if (AudioAddOpen != null)
                AudioAddOpen.Play();
        }
    }
}
