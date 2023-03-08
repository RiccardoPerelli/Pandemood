using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public class TutorialBlock : MonoBehaviour
{

    public GameObject dialogManager4;

    // Update is called once per frame
    void Update()
    {
        if (dialogManager4.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        if (DoNotDeleteInfo.GETLevelNo() >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
