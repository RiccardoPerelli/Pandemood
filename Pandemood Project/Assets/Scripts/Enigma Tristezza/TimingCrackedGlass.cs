using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingCrackedGlass : MonoBehaviour
{
    public float timeToStartCrack = 10f;
    public float timeToAddCrack = 7f;
    public GameObject[] CrackedGlass;
    public GameObject CrackGlassCharacter;
    public GameObject WaterMirrorSurface;
    public Transform WaterSurface;
    public Transform CharacterFoot;
    public GameObject Character;
    public GameObject RocksUnderWater;
    public Transform TeleportPosition;
    public GameObject Camera;

    //AUDIO
    public AudioSource CrackGlass;
    public AudioSource FinalCrack;

    //TRANSITION FLASH
    public GameObject FlashImage;

    bool start_crack = false;
    int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        RocksUnderWater.SetActive(false);
        CrackGlass.Stop();
        FinalCrack.Stop();
        StartCoroutine(StartCrack());
    }

    private IEnumerator StartCrack()
    {
        yield return new WaitForSeconds(timeToStartCrack);
        start_crack = true;
        StartCoroutine(StartCracking());
    }

    //OGNI TOT SECONDI ROMPE UN NUOVO VETRO
    private IEnumerator StartCracking()
    {

        while (start_crack)
        {
            yield return new WaitForSeconds(timeToAddCrack);
            if (n < CrackedGlass.Length)
            {
                CrackedGlass[n].SetActive(true);
                CrackGlass.Play();
            }
            n++;
            if (n>CrackedGlass.Length)
            {
                start_crack = false;
                //qui compare il crack del personaggio
                CrackGlassCharacter.transform.position = new Vector3 (CharacterFoot.position.x, CharacterFoot.position.y+0.08f, CharacterFoot.position.z);
                CrackGlassCharacter.SetActive(true); //crack buco
                FinalCrack.Play();
                Destroy(WaterMirrorSurface.GetComponent<Collider>());

                StartCoroutine(ChangeCharacterPosition()); //TELETRASPORA PERSONAGGIO
            }
        }
    }

    private IEnumerator ChangeCharacterPosition()
    {
        yield return new WaitForSeconds(0.1f);
        FlashImage.GetComponent<Flash>().doCameraFlash = true; //effetto flash
        //GameObject character=Instantiate(Character, TeleportPosition.position, Quaternion.Euler(0,180,0));
        //Destroy(Character);
        Character.transform.position = TeleportPosition.position;
        Character.GetComponent<SwimBehaviour>().checkWater = true;
        Destroy(WaterMirrorSurface);
        WaterSurface.position = new Vector3(WaterSurface.position.x, WaterSurface.position.y + 150f, WaterSurface.position.z); //alzo livello di acqua
        RocksUnderWater.SetActive(true);
        Destroy(gameObject);
    }
}
