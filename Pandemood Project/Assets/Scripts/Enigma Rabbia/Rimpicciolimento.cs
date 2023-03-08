using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rimpicciolimento : MonoBehaviour
{
    private bool shrink=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shrink"))
            if (!shrink)
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                shrink = true;
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                shrink = false;
            }
    }
}
