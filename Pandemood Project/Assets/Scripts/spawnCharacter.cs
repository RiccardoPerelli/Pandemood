using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCharacter : MonoBehaviour
{
    public Camera _camera;
    public GameObject male;

    public GameObject female;

    public int chooseCharacter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        chooseCharacter = PlayerPrefs.GetInt("Character");
        if (chooseCharacter == 0)
        {
            var newCharacter = Instantiate(male, this.transform.position, Quaternion.identity);
            newCharacter.transform.parent = gameObject.transform;
            _camera.GetComponent<CameraFollowTarget>().target = newCharacter.transform;
        } else if (chooseCharacter == 1)
        {
            var newCharacter = Instantiate(female, this.transform.position, Quaternion.identity);
            newCharacter.transform.parent = gameObject.transform;
            _camera.GetComponent<CameraFollowTarget>().target = newCharacter.transform;
        }
        
    }
}
