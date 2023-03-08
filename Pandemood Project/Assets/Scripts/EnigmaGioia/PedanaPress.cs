using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedanaPress : MonoBehaviour
{
    public float heightPress=0.05f;
    public AudioSource AudioPedana;

    private bool press=false;

    void OnTriggerEnter()
    {
        if (!press)
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y - heightPress, position.z);
            transform1.position = position;
            if (AudioPedana != null)
                AudioPedana.Play();
            press = true;
        }
    }

    void OnTriggerExit()
    {
        if (press)
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y + heightPress, position.z);
            transform1.position = position;
            if (AudioPedana != null)
                AudioPedana.Play();
            press = false;
        }
    }
}
