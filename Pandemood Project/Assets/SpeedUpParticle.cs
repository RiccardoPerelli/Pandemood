using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpParticle : MonoBehaviour
{

    private ParticleSystem _elevatorPSys;
    public float SimulationSpeedMultiplier = 10f;
    private bool _PlayerIsHere = false;

    private void Start()
    {
        _elevatorPSys = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsHere)
        {
            var main = _elevatorPSys.main;
            main.simulationSpeed = SimulationSpeedMultiplier;
        }
        else
        {
            var main = _elevatorPSys.main;
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
