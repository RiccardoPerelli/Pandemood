using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLifebuoy : MonoBehaviour
{
    public Transform target;
    public Material TransparentGreen;
    public float speed = 0.5f;

    public GameObject LifeBuoy;

    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeBuoy.GetComponent<LightObject>().illuminated)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist < 0.01f)
            {
                GetComponent<Renderer>().material = TransparentGreen; //change material
                if(target.gameObject.GetComponent<Shape>()!=null)
                    target.gameObject.GetComponent<Shape>().ShapeInsert(); //shape insert correctly
                Destroy(this);
            }
        }
        if (!LifeBuoy.GetComponent<LightObject>().illuminated)
            transform.position = initialPosition;
    }
}
