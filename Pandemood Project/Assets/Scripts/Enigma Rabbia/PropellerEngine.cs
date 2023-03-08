using System.Collections;
using System.Collections.Generic;
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
            Door.transform.position = Vector3.MoveTowards(Door.transform.position, TargetOpen.position, speedOpen * Time.deltaTime);
        }

        if(!open)
        {
            Door.transform.position = Vector3.MoveTowards(Door.transform.position, TargetStart, speedOpen * Time.deltaTime);
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
