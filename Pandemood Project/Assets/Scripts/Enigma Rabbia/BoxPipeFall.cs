using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPipeFall : MonoBehaviour
{
    private Rigidbody rb;
    public Transform TargetPointFall;
    public GameObject Pipe;

    private bool fall = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,TargetPointFall.position) < 0.05f && ! fall)
        {
            fall = true;
            transform.parent = null;
            rb.isKinematic = false;
            Pipe.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(Falling());
        }
    }

    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(0.7f);
        Pipe.GetComponent<PipeClosure>().active = false;
        Destroy(this);

    }
}
