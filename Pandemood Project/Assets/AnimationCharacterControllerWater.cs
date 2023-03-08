using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacterControllerWater : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) >= 0.1f || Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f)
        {
            GetComponent<Animator>().SetBool("IsSwimming", true);
        } else
        {
            GetComponent<Animator>().SetBool("IsSwimming", false);
        }
    }
}
