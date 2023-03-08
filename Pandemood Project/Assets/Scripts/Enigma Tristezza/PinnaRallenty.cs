using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinnaRallenty : MonoBehaviour
{
    public GameObject FishAnimator;
    public GameObject ShadowPinna;
    public GameObject Shape;
    public Material TransparentGreen;
    public float distancePut = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FishAnimator.GetComponent<RallentyTime>().rallenty)
            ShadowPinna.SetActive(true);
        else
            ShadowPinna.SetActive(false);

        if (FishAnimator.GetComponent<RallentyTime>().rallenty && ShadowPinna.GetComponent<LightObject>().illuminated)
        {
            float distance = Vector3.Distance(transform.position, Shape.transform.position);
            if (distance < distancePut)
            {
                GetComponent<Renderer>().material = TransparentGreen; //change material
                if (Shape.GetComponent<Shape>() != null)
                    Shape.GetComponent<Shape>().ShapeInsert(); //shape insert correctly
                transform.position = Shape.transform.position;
                transform.parent = Shape.transform;

                Destroy(ShadowPinna);
                Destroy(this);
            }
        }
    }
}
