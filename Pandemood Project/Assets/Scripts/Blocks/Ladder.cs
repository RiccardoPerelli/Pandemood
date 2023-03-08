using System;
using UnityEngine;

namespace Blocks
{
    public class Ladder : MonoBehaviour
    {

        [SerializeField] private PressurePlate plate;
        // Start is called before the first frame update
        void Start()
        {
            plate.ONPressure += Spawn;
            gameObject.SetActive(false);
        }

        

        private void Spawn(object sender, EventArgs args)
        {
            gameObject.SetActive(true);
        }
    }
}
