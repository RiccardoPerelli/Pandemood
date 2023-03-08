
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerCanvas : MonoBehaviour
{
    public string jumpButton = "Jump";
    public GameObject contTast;
    public GameObject contContr;
    public VideoPlayer vP;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetJoystickNames().Length > 0) //se joystick
            contTast.SetActive(false);
        else
            contContr.SetActive(false);
        
        vP.loopPointReached += LoadScene;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Awake()
    {
        StartCoroutine(FindObjectOfType<SubtitleDisplayer>().Begin());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpButton))
        {
            DoNotDeleteInfo.SetSceneToLoad(5);
            SceneManager.LoadScene(8);
        }

    }
    void LoadScene(VideoPlayer vp)
    {
        DoNotDeleteInfo.SetSceneToLoad(5);
        SceneManager.LoadScene(8);
    }
}

    