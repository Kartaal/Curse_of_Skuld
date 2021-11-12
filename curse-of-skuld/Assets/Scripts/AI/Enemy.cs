using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Chase,
        Search
        
    }
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool loopPatrol;
    [SerializeField] private Transform [] patrolTargets;
   
    private int _arrayDir;
    private int _curr;

    private bool _searching;
    
    private Transform _playerTransform;
    private Vector3 _lastKnownLocation;

    private State _state;


    private NavMeshAgent _agent;
    void Awake()
    {
        _searching = false;
        _state = State.Patrol;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemyData.MoveSpeed;
        _agent.autoBraking = false;
        _curr = 0;
        _arrayDir = 1;
        _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        
        switch (_state)
        {
            case State.Patrol:
                Debug.Log("Patrol");
                Patrol();
                break;
            
            case State.Chase:
                Debug.Log("Chase");
                Chase();
                if (_playerTransform == null)
                {
                    StartCoroutine(ResumePatrol());
                }
                else if (_playerTransform.position != _lastKnownLocation)
                {
                    Debug.Log("yay");
                    _state = State.Search;
                }
                break;
                
            case State.Search:
                Debug.Log("Search");
                if (!_searching)
                    StartCoroutine(StartSearch(2f));
                Search();
                break;
        }
    }

    private void Patrol()
    {
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f)
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
                    _arrayDir = -1;
                }
                else if (_curr == 0)
                {
                    _arrayDir = 1;
                }

                _curr += _arrayDir;
            }
        }
        
    }

    private void Chase()
    {
        _agent.speed = enemyData.ChaseSpeed;
        _agent.destination = _lastKnownLocation;
        
    }

    private IEnumerator ResumePatrol()
    {
        yield return new WaitForSeconds(1f);
        _state = State.Patrol;
    }

    private void Search()
    {
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            Vector3 searchDir = (_playerTransform.position - transform.position).normalized;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(searchDir * 5, out hit, 10, NavMesh.AllAreas))
                _agent.destination = hit.position;
        }
    }
    private IEnumerator StartSearch(float searchDuration)
    {
        _searching = true;
        _agent.speed = enemyData.MoveSpeed;
        yield return new WaitForSeconds(searchDuration);
        _agent.ResetPath();
        _searching = false;
        _state = State.Patrol;
    }
    public NavMeshAgent Agent => _agent;

    public void PlayerSpotted(Vector3 playerPos)
    {
        _state = State.Chase;
        _lastKnownLocation = playerPos;
    }
}
