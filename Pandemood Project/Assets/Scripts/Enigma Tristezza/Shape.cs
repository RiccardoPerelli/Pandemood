using System.Collections;
using System.Collections.Generic;
using Gamepad;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int NumberShapes;
    public bool completed=false;
    public GameObject ShapeDoor;
    public AudioSource AudioWall;
    public AudioSource AudioComplete;
    public Material TransparentGreen;
    private int n=0;

    private bool open = false;
    private Vector3 target;

    void Start()
    {
        var position = ShapeDoor.transform.position;
        target = new Vector3(position.x, position.y, position.z + 3f);
    }

    void Update()
    {
        if (open)
        {
            float step = 3f * Time.deltaTime; // calculate distance to move
            var position = ShapeDoor.transform.position;
            position = Vector3.MoveTowards(position, target, step);
            ShapeDoor.transform.position = position;
            float dist = Vector3.Distance(position, target);
            if (dist < 0.01f)
            {
                Destroy(this);
                MyGamepad.StopVibration();
            }
        }
    }

    public void ShapeInsert()
    {
        n++;
        if (n == NumberShapes)
        {
            completed = true;
            GetComponent<Renderer>().material = TransparentGreen;
            if(AudioWall!=null)
                AudioWall.Play();
            if (AudioComplete != null)
                AudioComplete.Play();
            open = true;
            MyGamepad.SetVibration(MyGamepad.DoorVibration);
        }
    }
}
