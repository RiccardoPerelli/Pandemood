using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public float DistanceIllumination=5f;
    public bool lightOn = false;
    public AudioSource AudioLightOn;

    private GameObject illuminationTexture;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(DistanceIllumination, DistanceIllumination, DistanceIllumination);
        GetComponent<Renderer>().enabled = false;
        illuminationTexture = transform.GetChild(0).gameObject;
        illuminationTexture.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (lightOn) //spegni
            {
                lightOn = false;
                GetComponent<Renderer>().enabled = false;
                illuminationTexture.GetComponent<Renderer>().enabled = false;
            }
            else //accendi
            {
                lightOn = true;
                GetComponent<Renderer>().enabled = true;
                illuminationTexture.GetComponent<Renderer>().enabled = true;
                AudioLightOn.Play();
            }
    }
}
