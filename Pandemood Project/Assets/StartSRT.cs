using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;

public class StartSRT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindObjectOfType<SubtitleDisplayer>().Begin());
    }
}
