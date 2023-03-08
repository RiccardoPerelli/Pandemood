using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTargetInGame : MonoBehaviour
{
    public Transform _target;

    private float _speed = 5.0f;
    private const float _epsilon = 0.1f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.LookAt(_target.position);
        if((transform.position - _target.position).magnitude > _epsilon)
            transform.Translate(0.0f, 0.0f, _speed*Time.deltaTime);
    }
}
