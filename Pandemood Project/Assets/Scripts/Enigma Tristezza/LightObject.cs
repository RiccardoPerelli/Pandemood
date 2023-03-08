using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public GameObject Illumination;
    public GameObject particles;
    public AudioSource AudioShadow;
    public float DistanceIllumination = 5f;
    private GameObject Player;

    public bool illuminated=false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance < DistanceIllumination && Illumination.GetComponent<LightOnOff>().lightOn) //SCOMPARE OGGETTO, LUCE ACCESA
        {
            GetComponent<Collider>().enabled = false;
            if(GetComponent<Renderer>()!=null)
                GetComponent<Renderer>().enabled = false;
            if(particles!=null)
                particles.SetActive(false);
            if (!illuminated)
                AudioShadow.Play();

             illuminated = true;
        }
        else //COMPARE OGGETTO, LUCE SPENTA
        {
            GetComponent<Collider>().enabled = true;
            if (GetComponent<Renderer>() != null)
                GetComponent<Renderer>().enabled = true;
            if (particles != null)
                particles.SetActive(true);

            if (illuminated)
                AudioShadow.Play();
            illuminated = false;
        }
    }
}
