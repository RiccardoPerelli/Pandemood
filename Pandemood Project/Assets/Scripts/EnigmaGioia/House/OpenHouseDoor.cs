using System.Collections;
using System.Collections.Generic;
using Gamepad;
using UnityEngine;

public class OpenHouseDoor : MonoBehaviour
{
    public GameObject HouseDoor;
    public AudioSource AudioComplete;
    public Material TransparentGreen_mat;

    private bool _open = false;
    private Vector3 _target;
    private GetNearInteractableObjectJoy _nearObject;

    void Start()
    {
        var position = HouseDoor.transform.position;
        _target = new Vector3(position.x, position.y, position.z + 3f);
        _nearObject = GetComponent<GetNearInteractableObjectJoy>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_nearObject.objectTaken)
        {
            _nearObject.objectTaken = false;
            if (AudioComplete != null)
                AudioComplete.Play();
            _open = true;
            MyGamepad.SetVibration(MyGamepad.DoorVibration, MyGamepad.DoorVibration);
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material = TransparentGreen_mat;
        }

        if (_open)
        {
            float step = 3f * Time.deltaTime; // calculate distance to move
            var position = HouseDoor.transform.position;
            position = Vector3.MoveTowards(position, _target, step);
            HouseDoor.transform.position = position;
            float dist = Vector3.Distance(position, _target);
            if (dist < 0.01f)
            {
                MyGamepad.StopVibration();
                Destroy(this);
            }
        }

    }
}
