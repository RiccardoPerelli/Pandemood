using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathResetComplete : MonoBehaviour
{
    public Material Transparent_mat;
    public Material RockGreen_mat;
    public Transform RockDoor;
    public AudioSource AudioWall;
    public AudioSource AudioReset;
    public GameObject[] cells;

    private bool reset = false;

    private bool open = false;
    private Vector3 target1,target2;

    void Start()
    {
        target1 = new Vector3(RockDoor.transform.position.x, RockDoor.transform.position.y, RockDoor.transform.position.z + 3f);
        target2 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);
    }

    void Update()
    {
        if (open)
        {
            float step = 3f * Time.deltaTime; // calculate distance to move
            RockDoor.transform.position = Vector3.MoveTowards(RockDoor.transform.position, target1, step);
            transform.position = Vector3.MoveTowards(transform.position, target2, step);
            float dist = Vector3.Distance(RockDoor.transform.position, target1);
            if (dist < 0.01f)
            {
                Destroy(this);
            }
        }
        else
            ControlIfComplete();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Reset();
            AudioReset.Play();
            GetComponent<DrawLine>().ResetLine();
        }
    }

    void Reset()
    {
        for (int i = 0; i < cells.Length && !open; i++) {
            cells[i].GetComponent<Collider>().isTrigger = true;
            cells[i].GetComponent<ChangeMaterialPath>().activate = false;
            cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = Transparent_mat;
        }
    }

    void ControlIfComplete()
    {
        int n = 0;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].GetComponent<ChangeMaterialPath>().activate)
                n++;
            else
                break;
        }

        if (n == cells.Length)
        {
            for (int i = 0; i < n; i++)
            {
                cells[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material = RockGreen_mat;
                Destroy(cells[i].GetComponent<ChangeMaterialPath>());
            }
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if(AudioWall!=null)
            AudioWall.Play();
        open = true;
    }
}
