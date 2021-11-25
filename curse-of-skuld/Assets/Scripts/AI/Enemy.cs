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
        Suspicious,
        Chase,
        Search
    }
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool loopPatrol;
    [SerializeField] private Transform[] patrolTargets;
    [SerializeField] private List<Transform> patrolStops;
    [SerializeField] private Animator anim;
   
    private int _arrayDir;
    private int _curr;
    private bool _waitRoutineRunning;

    private bool _searching;
    private bool _waiting;

    private float _timeSincePlayerLastVisible;

    private Transform _playerTransform;
    private Vector3 _lastKnownLocation;

    private State _state;


    private NavMeshAgent _agent;
    void Awake()
    {
        _searching = false;
        _waiting = false;
        _state = State.Patrol;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = enemyData.MoveSpeed;
        _agent.autoBraking = true;
        _waitRoutineRunning = false;
        _curr = 0;
        _arrayDir = 1;
        _playerTransform = FindObjectOfType<PlayerController>().gameObject.transform;
        _timeSincePlayerLastVisible = 1000;
    }

    private void Start()
    {
        if (patrolStops == null)
            patrolStops = new List<Transform>();
    }

    private void Update()
    {
        anim.SetBool("IsChasing", _state == State.Chase);

        _timeSincePlayerLastVisible += Time.deltaTime;

        switch (_state)
        {
            case State.Patrol:
                Patrol();
                break;
                
            case State.Suspicious:
                break;
            
            case State.Chase:
                if (_playerTransform.GetComponent<PlayerController>().Dead)
                {
                    StartCoroutine(ResumePatrol());
                    break;
                }
                else if (_playerTransform.position != _lastKnownLocation)
                {
                    _state = State.Search;
                    break;
                }
                Chase();
                break;
                
            case State.Search:
                if (!_searching)
                    StartCoroutine(StartSearch());
                Search();
                break;
        }
    }

    private void Patrol()
    {
        if (_waiting && !_waitRoutineRunning && _agent.remainingDistance <0.5f)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
        
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f && !_waiting)
        {
            _agent.destination = patrolTargets[_curr].position;
            if (patrolStops.Contains(patrolTargets[_curr]))
            {
                _waiting = true;
            }
            
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

    private IEnumerator WaitAtPatrolPoint()
    {
        _agent.ResetPath();
        _waitRoutineRunning = true;
        yield return new WaitForSeconds(enemyData.PatrolPointWaitTime);
        _waitRoutineRunning = false;
        _waiting = false;
    }

    private IEnumerator BecomeSuspicious(float visibilityPercentage)
    {
        _agent.ResetPath();
        yield return new WaitForSeconds(enemyData.MinTimeToChase + enemyData.VariableTimeToChase * (1-visibilityPercentage));
        _state = _timeSincePlayerLastVisible < enemyData.MaxTimeSincePlayerLastVisible ? State.Chase : State.Patrol;
    }

    private void Chase()
    {
        _agent.speed = enemyData.ChaseSpeed;
        _agent.destination = _lastKnownLocation;
        NavMeshHit hit;
        if (!_agent.Raycast(_playerTransform.position, out hit))
        {
            if (hit.distance < enemyData.KillDistance)
            {
                KillPlayer();
            }
        }
    }

    private void KillPlayer()
    {
        var player = _playerTransform.GetComponent<PlayerController>();
        player.Die();
    }

    private IEnumerator ResumePatrol()
    {
        yield return new WaitForSeconds(1f);
        _agent.speed = enemyData.MoveSpeed;
        _state = State.Patrol;
    }

    private void Search()
    {
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            Vector3 searchDir = (_playerTransform.position - transform.position).normalized;
            Vector3 searchLocation = _lastKnownLocation + searchDir * 3f;
            Vector2 random2DPoint = UnityEngine.Random.insideUnitCircle * 5f;
            Vector3 randomPointInSearchDir = searchLocation + new Vector3(random2DPoint.x, searchLocation.y, random2DPoint.y );
            
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPointInSearchDir, out hit, 1, NavMesh.AllAreas))
                _agent.destination = hit.position;
        }
    }
    private IEnumerator StartSearch()
    {
        _searching = true;
        _agent.speed = enemyData.MoveSpeed;
        yield return new WaitForSeconds(enemyData.SearchDuration);
        _agent.ResetPath();
        _searching = false;
        _state = State.Patrol;
    }

    public void PlayerSpotted(EnemyVisionData visionData)
    {
        if (_state != State.Suspicious && _state != State.Chase)
        {
            _state = State.Suspicious;
            StartCoroutine(BecomeSuspicious(visionData.VisibilityPercentage));
        }
        
        _timeSincePlayerLastVisible = 0;
        _lastKnownLocation = visionData.LastKnownPosition;
    }

    public void PlayerHeard(Vector3 playerPosition)
    {
        if(_state != State.Suspicious && _state != State.Chase)
        {
            _agent.ResetPath();
            _state = State.Chase;
        }
        _timeSincePlayerLastVisible = 0;
        _lastKnownLocation = playerPosition;
    }
}
