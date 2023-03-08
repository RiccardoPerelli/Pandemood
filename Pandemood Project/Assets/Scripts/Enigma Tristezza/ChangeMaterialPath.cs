<<<<<<< .merge_file_a21664
﻿using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class ChangeMaterialPath : MonoBehaviour
    {
        [FormerlySerializedAs("Transparent_mat")] public Material transparentMat;
        [FormerlySerializedAs("TransparentRed_mat")] public Material transparentRedMat;
        [FormerlySerializedAs("TransparentGreen_mat")] public Material transparentGreenMat;
        [FormerlySerializedAs("AudioError")] public AudioSource audioError;
        public bool activate = false;

        private GameObject _rock;
    

        // Start is called before the first frame update
        private void Start()
        {
            _rock = gameObject.transform.GetChild(0).gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("PlayerPath")) return;
            ChangeMaterialTo(transparentGreenMat, transparentRedMat, false);
            ChangeMaterialTo(transparentMat, transparentGreenMat, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerPath"))
            {
                //ChangeMaterialTo(TransparentGreen_mat, GreenRock_mat);
                //GetComponent<Collider>().isTrigger = false;
            }
        }

        // creates a new material instance that looks like the old material
        private void ChangeMaterialTo(Object matToControl, Material matChange, bool active)
        {
            var mat1 = _rock.GetComponent<Renderer>().material.name;
            var mat2 = matToControl.name;
            if (!mat1.Contains(mat2)) return;
            _rock.GetComponent<Renderer>().material = matChange;
            activate = active;
            var drawLine = transform.parent.gameObject.GetComponent<DrawLine>();
            if (activate)
            {
                //DRAW LINE
                if (drawLine != null)
                {
                    drawLine.SetNewPoint(transform);
=======
﻿using System.Collections;
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
>>>>>>> .merge_file_a26256
                }
            }
            else
                //DELETE LINE
<<<<<<< .merge_file_a21664
            if (drawLine != null){
                audioError.Play();
                drawLine.ResetLine();
            }
=======
                if (transform.parent.gameObject.GetComponent<DrawLine>() != null){
                    AudioError.Play();
                    transform.parent.gameObject.GetComponent<DrawLine>().ResetLine();
                }

>>>>>>> .merge_file_a26256
        }
    }
}
