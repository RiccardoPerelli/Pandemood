using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class Lumineo : MonoBehaviour
{   [SerializeField] Transform[] RouteLevel0;
    [SerializeField] Transform[] RouteLevel1;
    [SerializeField] Transform[] RouteLevel2;
    [SerializeField] Transform[] RouteLevel3;
    [SerializeField] Transform[] RouteLevel4;

    //public Transform Route;
    private Transform _startingPoint;
    private int _levelInfo;
    private int _route = 0;
    public Transform[] _routeToFollow;
    private float speed = 1f;
    private bool isLerp = false;
    private BoxCollider _boxCollider;
    private GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _levelInfo = DoNotDeleteInfo.GETLevelNo();
        ChooseRoute();

    }
    private void Update()
    {
        if (isLerp)
        {
            Debug.Log(Vector3.Distance(transform.position, _player.transform.position));
            _boxCollider.enabled = false;
            MoveMe();
            if (Vector3.Distance(transform.position, _player.transform.position) <= 4f)
                speed += 0.06f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Do Dialogue");
            //after dialogue
            isLerp = true;

        }
    }

    private void MoveMe()
    {
        transform.position = Vector3.MoveTowards(transform.position, _routeToFollow[_route].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _routeToFollow[_route].position) <= 0.1f)
        {
            if(_route+1<=_routeToFollow.Length)
            _route++;
            _boxCollider.enabled = true;
            speed = 1f;
            isLerp = false;
        }
    }

    private void ChooseRoute()
    {
        switch (_levelInfo)
        {
            case 0:
                _routeToFollow = RouteLevel0;
                break;
            case 1:
                _routeToFollow = RouteLevel1;
                break;
            case 2:
                _routeToFollow = RouteLevel2;
                break;
            case 3:
                _routeToFollow = RouteLevel3;
                break;
            case 4:
                _routeToFollow = RouteLevel4;
                break;
            default:
                return;
        }
    }
}
