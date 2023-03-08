using UnityEngine;

namespace General
{
    public class InteractableObject : MonoBehaviour
    {
        public bool pickedUp = false;
        public bool cantPickUp = false;
        public float distanceInteractionKeyImage = 2f;
        public GameObject KeyInteractionImage;

        private bool playerNear;
        GameObject Player;

        private void Start()
        {
            if(KeyInteractionImage!=null)
                KeyInteractionImage.SetActive(false);

            Player = GameObject.FindGameObjectWithTag("Player"); 
        }

        private void Update()
        {
            //VISIBLE KEY INTERACTION
            if(!pickedUp && playerNear)
            {
                if(KeyInteractionImage!=null)
                    KeyInteractionImage.SetActive(true);
            }

            //HIDE KEY INTERACTION
            if (pickedUp || !playerNear)
            {
                if (KeyInteractionImage != null)
                    KeyInteractionImage.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            //--------- CONSIDER THE CLOSEST OBJECT CAN PICK UP ----------
            if (Player != null)
                ControlDistanceFromPlayer();
        }


        //-----------SET CLOSEST OBJECT CAN PICK UP--------------
        private void ControlDistanceFromPlayer()
        {
            var diff =Vector3.Distance(Player.transform.position,transform.position);
            playerNear = diff < distanceInteractionKeyImage;
        }
    }
}
