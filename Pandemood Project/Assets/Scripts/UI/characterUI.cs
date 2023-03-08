using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterUI : MonoBehaviour
{
    public void setFemale()
    {
        PlayerPrefs.SetInt("Character", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void setMale()
    {
        PlayerPrefs.SetInt("Character", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
