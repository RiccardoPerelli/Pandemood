using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.Serialization;

public class Lumineo : MonoBehaviour
{
    [Header("Routes")] [SerializeField] Transform[] RouteLevel0;
    [SerializeField] Transform[] RouteLevel1;
    [SerializeField] Transform[] RouteLevel2;
    [SerializeField] Transform[] RouteLevel3;
    [SerializeField] BoxCollider _boxColliderdDialogue;

    //public Transform Route;
    private Transform _startingPoint;
    private int _levelInfo;
    private int _route = 0;
    private Transform[] _routeToFollow;
    [Header("Speed")] [SerializeField] private float speed = 1f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 25f;
    private bool _isLerp = false;
    private BoxCollider _boxCollider;
    private GameObject _player;
    private bool _hasPlayer = false;

    private void Start()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _levelInfo = DoNotDeleteInfo.GETLevelNo();
        ChooseRoute();
    }

    private void Update()
    {
        if (!_hasPlayer)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _hasPlayer = true;
        }

        if (_isLerp)
        {
            //Debug.Log(Vector3.Distance(transform.position, _player.transform.position));
            _boxCollider.enabled = false;
            MoveMe();
            Vector3 playerPos = _player.transform.position;
            playerPos.y = 0;
            Vector3 lumineoPos = transform.position;
            lumineoPos.y = 0;
            if (Vector3.Distance(playerPos, lumineoPos) <= 3f && !inGameMenu.GameIsPaused)
                speed += acceleration * Time.deltaTime;
            else if (!inGameMenu.GameIsPaused)
            {
                speed -= deceleration * Time.deltaTime;
                if (speed < 0) speed = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Do Dialogue");
            //after dialogue
            if (_routeToFollow == RouteLevel0 && (_route - 1) == 2)
            {
                _boxColliderdDialogue.enabled = true;
            }

            _isLerp = true;
        }
    }

    private void MoveMe()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, _routeToFollow[_route].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _routeToFollow[_route].position) <= 0.1f)
        {
            if (_route + 1 < _routeToFollow.Length)
            {
                _route++;
            }

            _boxCollider.enabled = true;
            speed = 1f;
            _isLerp = false;
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
                transform.position = _routeToFollow[0].position;
                break;
            case 2:
                _routeToFollow = RouteLevel2;
                transform.position = _routeToFollow[0].position;
                break;
            case 3:
                _routeToFollow = RouteLevel3;
                transform.position = _routeToFollow[0].position;
                break;
            default:
                return;
        }
    }
}