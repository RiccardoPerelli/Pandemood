using System.Collections;
using System.Collections.Generic;
using Gamepad;
using UnityEngine;

public class PropellerEngine : MonoBehaviour
{
    public int NToOpen;
    public Transform TargetOpen;
    public GameObject Door;
    public GameObject Blades;
    public float speedOpen = 5f;

    private bool open = false;
    private int n = 0;
    private Vector3 TargetStart;
    private Animator animator_blades;

    public AudioSource AudioCompleted;
    public AudioSource AudioAddOpen;
    public AudioSource AudioEngine;

    private bool _vibrating;

    void Start()
    {
        TargetStart = Door.transform.position;
        animator_blades = Blades.GetComponent<Animator>();
        animator_blades.enabled = false;
    }

    void Update()
    {
        if (open)
        {
            Door.transform.position =
                Vector3.MoveTowards(Door.transform.position, TargetOpen.position, speedOpen * Time.deltaTime);
            if (!_vibrating &&  Vector3.Distance(Door.transform.position, TargetOpen.position) > 0.01f)
            {
                _vibrating = true;
                MyGamepad.SetVibration(MyGamepad.DoorVibration);
            } else if (_vibrating && Vector3.Distance(Door.transform.position, TargetOpen.position) <= 0.01f)
            {
                _vibrating = false;
                MyGamepad.StopVibration();
            }
        }
        else
        {
            Door.transform.position =
                Vector3.MoveTowards(Door.transform.position, TargetStart, speedOpen * Time.deltaTime);
            if (!_vibrating &&  Vector3.Distance(Door.transform.position, TargetStart) > 0.01f)
            {
                _vibrating = true;
                MyGamepad.SetVibration(MyGamepad.DoorVibration);
            } else if (_vibrating && Vector3.Distance(Door.transform.position, TargetStart) <= 0.01f)
            {
                _vibrating = false;
                MyGamepad.StopVibration();
            }
        }
    }

    public void AddOpen()
    {
        n++;
        if (n == NToOpen) //open
        {
            open = true;
            animator_blades.enabled = true;
            if (AudioCompleted != null)
                AudioCompleted.Play();
            AudioEngine.Play();
        }

        if (n < NToOpen)
            if (AudioAddOpen != null)
                AudioAddOpen.Play();
    }

    public void SubstractOpen()
    {
        n--;
        open = false;
        AudioEngine.Stop();
        animator_blades.enabled = false;
    }
}