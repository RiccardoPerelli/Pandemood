using System;
using Character.Character_Controller;
using DDSystem.Script;
using Enigma_Rabbia;
using UnityEngine;

namespace General
{
    public class PickUpController : MonoBehaviour
    {
        public GameObject myHands; //reference to your hands/the position where you want your object to go
        public float distanceInteraction;
        public AudioSource AudioPickup;
        bool _canpickup; //a bool to see if you can or cant pick up the item
        GameObject _objectIwantToPickUp; // the gameobject closest to pickup
        GameObject _objectPickUp; //the gameobject pick up
        bool _hasItem; // a bool to see if you have an item in your hand

        private GameObject[] _interactableObjects;
        private AnimationCharacterController _push;
        private Rimpicciolimento _shrink;

        private bool _dialogOpen;
        private DialogManager[] _dialogManagers;


        // Start is called before the first frame update
        private void Start()
        {
            _canpickup = false;    //setting both to false
            _hasItem = false;
            _interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");  //find all objects interactable
            if(GameObject.FindGameObjectWithTag("PlayerAnimation")!=null)
                _push=GameObject.FindGameObjectWithTag("PlayerAnimation").GetComponent<AnimationCharacterController>();
            if(GameObject.FindGameObjectWithTag("Player")!=null)
                _shrink= GameObject.FindGameObjectWithTag("Player").GetComponent<Rimpicciolimento>();
            
            _dialogManagers = Resources.FindObjectsOfTypeAll<DialogManager>();
            foreach (var dialog in _dialogManagers)
            {
                dialog.OnActivate += OnDialogOpen;
                dialog.OnDeactivate += OnDialogClose;
            }
        }

        void OnDialogOpen(object o, EventArgs e)
        {
            _dialogOpen = true;
        }
        
        
        void OnDialogClose(object o, EventArgs e)
        {
            _dialogOpen = false;
        }
        // Update is called once per frame
        void Update()
        {

            //---------------RELEASE OBJECT--------------------
            if (Input.GetButtonDown("Interact") && _hasItem && !IsShrinking() && !IsPushing() && !inGameMenu.GameIsPaused && !_dialogOpen)
            {  
                _objectPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
                _objectPickUp.transform.parent = null; // make the object no be a child of the hands
                _objectPickUp.GetComponent<Collider>().enabled = true;
                _objectPickUp.GetComponent<Collider>().isTrigger = false;
                _hasItem = false;
                if (_objectPickUp.GetComponent<InteractableObject>())
                    _objectPickUp.GetComponent<InteractableObject>().pickedUp = false; //object release
                AudioPickup.Play();
            }
            else
            if (_canpickup) // if you enter the collider of the objecct
            {
                //----------------PICKUP OBJECT-------------------
                if (Input.GetButtonDown("Interact") && !IsShrinking() && !IsPushing()  && !inGameMenu.GameIsPaused && !_dialogOpen) 
                    if (_hasItem == false) {
                        _objectPickUp = _objectIwantToPickUp;
                        _objectPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                        _objectPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
                        _objectPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                        _objectPickUp.transform.rotation = Quaternion.identity;
                        _objectPickUp.GetComponent<Collider>().isTrigger = true;
                        _hasItem = true;
                        if(_objectPickUp.GetComponent<InteractableObject>())
                            _objectPickUp.GetComponent<InteractableObject>().pickedUp = true; //object picked up
                        AudioPickup.Play();
                    }
            }
        }

        void FixedUpdate()
        {
            //--------- CONSIDER THE CLOSEST OBJECT CAN PICK UP ----------
            if (_interactableObjects!=null)
                ControlDistanceFromObject();
        }


        //-----------SET CLOSEST OBJECT CAN PICK UP--------------
        private void ControlDistanceFromObject()
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;

            foreach (GameObject obj in _interactableObjects)
            {
                if (!obj.GetComponent<InteractableObject>().cantPickUp)
                {
                    float curDistance = Vector3.Distance(obj.transform.position, transform.position);
                    if (curDistance < distance && curDistance < distanceInteraction)
                    {
                        closest = obj;
                        distance = curDistance;
                    }
                }
            }

            if (closest != null)
            {
                _objectIwantToPickUp = closest;
                _canpickup = true;
            }
            else
                _canpickup = false;
        }


        bool IsShrinking()
        {
            if (_shrink != null)
                return _shrink.shrink;
            return false;
        }

        bool IsPushing()
        {
            if (_push != null)
                return _push._isPushing;
            return false;
        }
    }
}
