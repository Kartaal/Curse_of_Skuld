using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool loopPatrol;
    [SerializeField] private Transform [] patrolTargets;
   
    private int _arrayDir;
    private int _curr;
    private Transform _playerTransform;


    private NavMeshAgent _agent;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemyData.MoveSpeed;
        _agent.autoBraking = false;
        _curr = 0;
        _arrayDir = 1;
        _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            //Patrol();
        }
    }

    private void Patrol()
    {
        if (_agent.speed != enemyData.MoveSpeed)
        {
            _agent.speed = enemyData.MoveSpeed;
        }
        
        _agent.destination = patrolTargets[_curr].position;

        if (loopPatrol)
        {
            _curr = (_curr + _arrayDir) % patrolTargets.Length;
        }
        else
        {
            if (_curr == patrolTargets.Length - 1)
            {
                _arrayDir = -1 ;
            }
            else if (_curr == 0)
            {
                _arrayDir = 1;
            }

            _curr += _arrayDir;
        }
        
    }

    private void Chase()
    {

    }
    public NavMeshAgent Agent => _agent;

    public Transform PlayerTransform
    {
        get => _playerTransform;
        set => _playerTransform = value;
    }
}
