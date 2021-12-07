using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool loopPatrol;
    [SerializeField] private PatrolWaypoint[] patrolTargets;
    [SerializeField] private Animator anim;

    private int _arrayDir;
    private int _curr;
    private bool _waitRoutineRunning;

    private bool _searching;
    private bool _waiting;

    private float _timeSincePlayerLastVisible;

    private Transform _playerTransform;
    private Vector3 _lastKnownLocation;

    private EnemyState _state;


    private NavMeshAgent _agent;

    private EventInstance _passiveMonsterSound;
    private EventInstance _alertMonsterSound;


    void Awake()
    {
        _searching = false;
        _waiting = false;
        _state = EnemyState.Patrol;
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
        _passiveMonsterSound = RuntimeManager.CreateInstance(AudioManager.Instance.monsterPassive);
        _alertMonsterSound = RuntimeManager.CreateInstance(AudioManager.Instance.monsterAlert);
        this.LowerVolumeOnSounds();

        RuntimeManager.AttachInstanceToGameObject(_passiveMonsterSound, this.transform);
        RuntimeManager.AttachInstanceToGameObject(_alertMonsterSound, this.transform);
    }

    private void OnDestroy()
    {
        _passiveMonsterSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _alertMonsterSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void Update()
    {
        if (CheckIfPlayerInHearingRange())
        {
            this.IncreaseVolumeOnSounds();
        }
        else
        {
            this.LowerVolumeOnSounds();
        }

        DrawPath();
        anim.SetBool("IsChasing", _state == EnemyState.Chase);
        anim.SetBool("IsSuspicious", _state == EnemyState.Suspicious);

        _timeSincePlayerLastVisible += Time.deltaTime;

        switch (_state)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
                
            case EnemyState.Suspicious:
                break;
            
            case EnemyState.Chase:
                if (_playerTransform.GetComponent<PlayerController>().Dead)
                {
                    StartCoroutine(ResumePatrol());
                    break;
                }
                else if (_playerTransform.position != _lastKnownLocation)
                {
                    _state = EnemyState.Search;
                    break;
                }
                Chase();
                break;
                
            case EnemyState.Search:
                if (!_searching)
                    StartCoroutine(StartSearch());
                Search();
                break;
        }

        if (_state == EnemyState.Chase)
        {
            this.PlaySoundInstanceIfNotAlreadyRunning(_alertMonsterSound);
            this.StopSoundInstanceIfNotAlreadyStopped(_passiveMonsterSound);
        } 
        else
        {
            this.PlaySoundInstanceIfNotAlreadyRunning(_passiveMonsterSound);
            this.StopSoundInstanceIfNotAlreadyStopped(_alertMonsterSound);
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
            _agent.destination = patrolTargets[_curr].PatrolPoint.position;
            if (patrolTargets[_curr].WaitTime > 0)
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
        _agent.updateRotation = false;
        transform.LookAt(patrolTargets[_curr].ViewDir);
        yield return new WaitForSeconds(enemyData.PatrolPointWaitTime);
        _agent.updateRotation = true;
        _waitRoutineRunning = false;
        _waiting = false;
    }

    private IEnumerator BecomeSuspicious()
    {
        _agent.ResetPath();
        yield return new WaitForSeconds(enemyData.MinTimeToChase);
        _state = _timeSincePlayerLastVisible < enemyData.MaxTimeSincePlayerLastVisible ? EnemyState.Chase : EnemyState.Patrol;
    }

    private void Chase()
    {
        _agent.speed = enemyData.ChaseSpeed;
        _agent.destination = _lastKnownLocation;
        NavMeshHit hit;
        if (!_agent.Raycast(_playerTransform.position, out hit))
        {
            Debug.DrawLine(transform.position, hit.position, Color.yellow);
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
        _state = EnemyState.Patrol;
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
        _state = EnemyState.Patrol;
    }

    public void PlayerSpotted(EnemyVisionData visionData)
    {
        if (_state != EnemyState.Suspicious && _state != EnemyState.Chase)
        {
            _state = EnemyState.Suspicious;
            StartCoroutine(BecomeSuspicious());
        }

        _timeSincePlayerLastVisible = 0;
        _lastKnownLocation = visionData.LastKnownPosition;
    }


    public NavMeshAgent Agent => _agent;

    public EnemyState State => _state;

    private bool CheckIfPlayerInHearingRange()
    {
        var vectorToPlayerCamera = SystemManager.Instance.playerGameObject.GetComponentInChildren<Animator>().transform.position - this.transform.position;

        //avoid costly root function
        var magnitude = vectorToPlayerCamera.magnitude;
        if (magnitude <= 13)
        {
            RaycastHit hit;
            int layerMask = 1 << 8;
            return !Physics.Raycast(transform.position, vectorToPlayerCamera, out hit, magnitude, layerMask);
        }

        return false;
    }

    private void LowerVolumeOnSounds()
    {
        _alertMonsterSound.setVolume(0.1f);
        _passiveMonsterSound.setVolume(0.1f);
    }

    private void IncreaseVolumeOnSounds()
    {
        _alertMonsterSound.setVolume(1f);
        _passiveMonsterSound.setVolume(1f);
    }

    private void PlaySoundInstanceIfNotAlreadyRunning(EventInstance instance)
    {
        PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        
        if (state != PLAYBACK_STATE.PLAYING)
        {
            instance.start();
        }
        
    }

    private void StopSoundInstanceIfNotAlreadyStopped(EventInstance instance)
    {
        PLAYBACK_STATE state;
        instance.getPlaybackState(out state);

        if (state != PLAYBACK_STATE.STOPPED)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    private void DrawPath()
    {
        var pathPoints = _agent.path.corners;
        for (int i = 0; i < pathPoints.Length-1; i++)
        {
            Debug.DrawLine(pathPoints[i], pathPoints[i+1], Color.cyan);
        }
    }
}
