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


    private NavMeshAgent _agent;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemyData.MoveSpeed;
        _agent.autoBraking = false;
        _curr = 0;
        _arrayDir = 1;
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
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
}
