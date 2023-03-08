using System.Collections;
using System.Collections.Generic;
using Gamepad;
using UnityEngine;
using XInputDotNetPure;

public class OpenPerlDoor : MonoBehaviour
{
    public GameObject PerlDoor;
    public AudioSource AudioWall;
    public AudioSource AudioComplete;

    private bool open=false;
    private Vector3 target;

    void Start()
    {
        var position = PerlDoor.transform.position;
        target= new Vector3(position.x, position.y, position.z + 3f);
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GetNearInteractableObjectSad>().objectTaken)
        {
            GetComponent<GetNearInteractableObjectSad>().objectTaken = false;
            if (AudioWall != null)
                AudioWall.Play();
            if (AudioComplete != null)
                AudioComplete.Play();
            open = true;
            MyGamepad.SetVibration(MyGamepad.DoorVibration, MyGamepad.DoorVibration);
        }

        if (open)
        {
            float step = 3f * Time.deltaTime; // calculate distance to move
            var position = PerlDoor.transform.position;
            position = Vector3.MoveTowards(position, target, step);
            PerlDoor.transform.position = position;
            float dist = Vector3.Distance(position, target);
            if (dist < 0.01f)
            {
                Destroy(this);
                MyGamepad.SetVibration(0, 0);
            }
        }

    }
}
