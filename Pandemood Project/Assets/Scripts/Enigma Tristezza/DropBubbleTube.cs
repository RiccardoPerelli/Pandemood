using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBubbleTube : MonoBehaviour
{
    public GameObject Bubble;
    public float timeDropBubble = 5f;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropBubble());
        Player=GameObject.FindWithTag("Player");
    }
    
    void FixedUpdate()
    {
        Bubble.transform.position = transform.position;
    }

    private IEnumerator DropBubble()
    {
        yield return new WaitForSeconds(timeDropBubble);
        if(Vector3.Distance(Player.transform.position, transform.position) < 30f) { //START BUBBLING ONLY IF PLAYER NEAR
            GameObject bubble = Instantiate(Bubble);
            bubble.transform.position = transform.position;
            bubble.GetComponent<BubbleDropped>().speed = 1f;
            if (Bubble.GetComponent<RallentyTime>() != null) //start in rallenty if needed
                if (Bubble.GetComponent<RallentyTime>().rallenty)
                    bubble.GetComponent<RallentyTime>().rallenty = true;
        }
        StartCoroutine(DropBubble());
    }
}
