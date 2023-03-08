using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsMenu : MonoBehaviour
{
    public AudioMixer _audioMixer;
    
    public void setVolume (float volume)
    {
        _audioMixer.SetFloat("MusicVol", volume);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen=isFullScreen;
    }

    public void saveSettings()
    {
        //TODO: Trovare un modo per salvare le impostazioni di luminosità tra scene
    }
}
