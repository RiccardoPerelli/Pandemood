using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpParticle : MonoBehaviour
{

    public ParticleSystem elevatorPSys;
    public float SimulationSpeedMultiplier = 10f;
    private bool _PlayerIsHere = false;

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsHere)
        {
            float _vValue = Input.GetAxisRaw("Vertical");
            if (_vValue >= 0.1f) {
                var main = elevatorPSys.main;
                main.simulationSpeed = _vValue * SimulationSpeedMultiplier;
            }
            else
            {
                var main = elevatorPSys.main;
                main.simulationSpeed = 1f;
            }
        }
        else
        {
            var main = elevatorPSys.main;
            main.simulationSpeed = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            _PlayerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _PlayerIsHere = false;
        }
    }
}
