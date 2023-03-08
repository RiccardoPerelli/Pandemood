using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallentyTime : MonoBehaviour
{
    public bool rallenty=false;
    private Animator animator;
    private BubbleDropped bubble_dropped;
    private BubbleLifebuoy bubble_lifebuoy;
    private DropBubbleTube drop_bubble_tube;
    private NVBoids boids;
    private float startVelocity;
    public float rallentySpeed=0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
            startVelocity = animator.speed;
        }
        if (GetComponent<BubbleDropped>() != null)
        {
            bubble_dropped = GetComponent<BubbleDropped>();
            startVelocity = bubble_dropped.speed;
        }

        if (GetComponent<BubbleLifebuoy>() != null)
        {
            bubble_lifebuoy = GetComponent<BubbleLifebuoy>();
            startVelocity = bubble_lifebuoy.speed;
        }

        if (GetComponent<DropBubbleTube>() != null)
        {
            drop_bubble_tube = GetComponent<DropBubbleTube>();
            startVelocity = drop_bubble_tube.timeDropBubble;
        }

        if (GetComponent<NVBoids>() != null)
        {
            boids = GetComponent<NVBoids>();
            startVelocity = boids.birdSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //RALLENTY
        if (Input.GetMouseButtonDown(1))
            if (!rallenty) //RALLENTY
                rallenty = true;
            else  //NO RALLENTY
                rallenty = false;

        if (rallenty)
        {
            if (bubble_dropped != null)
                bubble_dropped.speed = rallentySpeed;
            if (animator != null)
                animator.speed = rallentySpeed;
            if (bubble_lifebuoy != null)
                bubble_lifebuoy.speed = rallentySpeed;
            if (drop_bubble_tube!= null)
                drop_bubble_tube.timeDropBubble = rallentySpeed;
            if (boids != null)
                boids.birdSpeed = rallentySpeed/4;
        }
        else
        {
            if (bubble_dropped != null)
                bubble_dropped.speed = startVelocity;
            if (animator != null)
                animator.speed = startVelocity;
            if (bubble_lifebuoy != null)
                bubble_lifebuoy.speed = startVelocity;
            if (drop_bubble_tube != null)
                drop_bubble_tube.timeDropBubble = startVelocity;
            if (boids != null)
                boids.birdSpeed = startVelocity;
        }


    }

}
