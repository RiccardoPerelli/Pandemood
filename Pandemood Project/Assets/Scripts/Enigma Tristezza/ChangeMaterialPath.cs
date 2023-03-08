using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialPath : MonoBehaviour
{
    public Material Transparent_mat;
    public Material TransparentRed_mat;
    public Material TransparentGreen_mat;
    public AudioSource AudioError;
    public bool activate = false;

    private GameObject Rock;
    

    // Start is called before the first frame update
    void Start()
    {
        Rock = gameObject.transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeMaterialTo(TransparentGreen_mat, TransparentRed_mat, false);
            ChangeMaterialTo(Transparent_mat, TransparentGreen_mat, true);        
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //ChangeMaterialTo(TransparentGreen_mat, GreenRock_mat);
            //GetComponent<Collider>().isTrigger = false;
        }
    }

    // creates a new material instance that looks like the old material
    public void ChangeMaterialTo(Material mat_to_control, Material mat_change, bool active)
    {
        string mat1 = Rock.GetComponent<Renderer>().material.name;
        string mat2 = mat_to_control.name;
        if (mat1.Contains(mat2))
        {
            Rock.GetComponent<Renderer>().material = mat_change;
            activate = active;
            if (activate)
            {
                //DRAW LINE
                if (transform.parent.gameObject.GetComponent<DrawLine>() != null)
                {
                    transform.parent.gameObject.GetComponent<DrawLine>().SetNewPoint(transform);
                }
            }
            else
                //DELETE LINE
                if (transform.parent.gameObject.GetComponent<DrawLine>() != null){
                    AudioError.Play();
                    transform.parent.gameObject.GetComponent<DrawLine>().ResetLine();
                }

        }
    }
}
