using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpParticle : MonoBehaviour
{

    private ParticleSystem _elevatorPSys;
    public float SimulationSpeedMultiplier = 10f;
    public float pitchOnPlayerHere = 1.25f;
    public float pitchWithoutPlayerHere = 1f;
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
            if(gameObject.GetComponent<AudioSource>() != null)
            {
                gameObject.GetComponent<AudioSource>().pitch = pitchOnPlayerHere;
            }
        }
        else
        {
            var main = _elevatorPSys.main;
            main.simulationSpeed = 1f;
            if (gameObject.GetComponent<AudioSource>() != null)
            {
                gameObject.GetComponent<AudioSource>().pitch = pitchWithoutPlayerHere;
            }
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
