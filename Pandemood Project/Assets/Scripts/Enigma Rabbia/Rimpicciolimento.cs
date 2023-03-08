using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rimpicciolimento : MonoBehaviour
{
    public delegate void Small();
    public static event Small SmallTriggered;
    public delegate void Big();
    public static event Big BigTriggered;

    public bool shrink=false;

    public GameObject particleEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shrink"))
            if (!shrink)
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                SmallTriggered?.Invoke();
                shrink = true;
                GameObject tmp = Instantiate(particleEffect, transform.position, transform.rotation);
                tmp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                BigTriggered?.Invoke();
                shrink = false;
                Instantiate(particleEffect, transform.position, transform.rotation);
            }
    }
}
