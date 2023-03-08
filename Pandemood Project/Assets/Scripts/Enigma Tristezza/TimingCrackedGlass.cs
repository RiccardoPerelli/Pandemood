using Character.Character_Controller;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enigma_Tristezza
{
    public class TimingCrackedGlass : MonoBehaviour
    {
        public float timeToStartCrack = 10f;
        public float timeToAddCrack = 7f;
        [Header("Water")]
        [FormerlySerializedAs("CrackedGlass")] public GameObject[] crackedGlass;
        [FormerlySerializedAs("CrackGlassCharacter")] public GameObject crackGlassCharacter;
        [FormerlySerializedAs("WaterMirrorSurface")] public GameObject waterMirrorSurface;
        [FormerlySerializedAs("WaterSurface")] public Transform waterSurface;
        [FormerlySerializedAs("RocksUnderWater")] public GameObject rocksUnderWater;
        [FormerlySerializedAs("WaterAnimatorController")] public RuntimeAnimatorController waterAnimator;
        [Header("Character")]
        [FormerlySerializedAs("camera")] [FormerlySerializedAs("Camera")] public GameObject internalCamera;
        [FormerlySerializedAs("TeleportPosition")] public Transform teleportPosition;
        [FormerlySerializedAs("CharacterFoot")] public Transform characterFoot;
        [FormerlySerializedAs("Character")] public GameObject character;

        //AUDIO
        [Header("Audio")]
        [FormerlySerializedAs("CrackGlass")] public AudioSource crackGlass;
        [FormerlySerializedAs("FinalCrack")] public AudioSource finalCrack;

        //TRANSITION FLASH
        [Header("Flash")]
        [FormerlySerializedAs("FlashImage")] public GameObject flashImage;

        bool _startCrack;
        int _n;

        // Start is called before the first frame update
        void Start()
        {
            rocksUnderWater.SetActive(false);
            crackGlass.Stop();
            finalCrack.Stop();
            StartCoroutine(StartCrack());
        }

        private IEnumerator StartCrack()
        {
            yield return new WaitForSeconds(timeToStartCrack);
            _startCrack = true;
            StartCoroutine(StartCracking());
        }

        //OGNI TOT SECONDI ROMPE UN NUOVO VETRO
        private IEnumerator StartCracking()
        {

            while (_startCrack)
            {
                yield return new WaitForSeconds(timeToAddCrack);
                if (_n < crackedGlass.Length)
                {
                    crackedGlass[_n].SetActive(true);
                    crackGlass.Play();
                }
                _n++;
                if (_n>crackedGlass.Length)
                {
                    _startCrack = false;
                    //qui compare il crack del personaggio
                    var position = characterFoot.position;
                    crackGlassCharacter.transform.position = new Vector3 (position.x, position.y+0.08f, position.z);
                    crackGlassCharacter.SetActive(true); //crack buco
                    finalCrack.Play();
                    Destroy(waterMirrorSurface.GetComponent<Collider>());

                    StartCoroutine(ChangeCharacterPosition()); //TELETRASPORTA PERSONAGGIO
                }
            }
        }

        private IEnumerator ChangeCharacterPosition()
        {
            yield return new WaitForSeconds(0.1f);
            flashImage.GetComponent<Flash>().doCameraFlash = true; //effetto flash

            character.transform.position = teleportPosition.position;
            character.GetComponent<Character.Character_Controller.PhysicsCharacterController>().enabled = false;
            character.transform.GetChild(0).GetComponent<Character.Character_Controller.AnimationCharacterController>().enabled = false;
            character.GetComponent<Rigidbody>().useGravity = false;
            character.GetComponentInChildren<Animator>().runtimeAnimatorController = waterAnimator;
            character.GetComponent<PhysicsCharacterControllerWater>().enabled = true;
            character.GetComponent<PhysicsCharacterControllerWater>().StartsToMoveHValue = 0.2f;
            character.GetComponent<PhysicsCharacterControllerWater>().turnSmoothtime = 0.06f;
            character.GetComponentInChildren<AnimationCharacterControllerWater>().enabled = true;
            character.GetComponent<CapsuleCollider>().height = 1.5f;
            character.GetComponent<CapsuleCollider>().radius = 0.2f;
            character.GetComponent<CapsuleCollider>().direction = 2;
            character.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
            FindObjectOfType<AnimationCharacterController>().isSwimming = true;
            character.transform.GetChild(0).transform.position = new Vector3(0, -2, 0) + character.transform.position;

            Destroy(waterMirrorSurface);
            var position = waterSurface.position;
            position = new Vector3(position.x, position.y + 150f, position.z); //alzo livello di acqua
            waterSurface.position = position;
            rocksUnderWater.SetActive(true);
            Destroy(gameObject);
        }
    }
}
