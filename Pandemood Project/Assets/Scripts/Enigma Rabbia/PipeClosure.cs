using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeClosure : MonoBehaviour
{
    public GameObject Particles;

    public bool active = true;

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            Particles.SetActive(false);
        }
        else
        {
            Particles.SetActive(true);

        }
    }
}
