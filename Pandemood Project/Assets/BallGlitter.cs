using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGlitter : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private ParticleSystem FX;
    [SerializeField] private GameObject particleEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            Instantiate(particleEffect, ball.transform.position, ball.transform.rotation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ball)
        {
            FX.Stop();
        }
    }
}
