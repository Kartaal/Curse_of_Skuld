using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private FOVData viewData;

    [SerializeField] private LayerMask playerMask;

    private Enemy _enemy;
    private List<Transform> _playerVisualization;

    private Transform[] _playerLimbs;
    private int _hitCount;
    private int _numberVisionTargets;
    NavMeshHit _hit;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _hitCount = 0;
        _playerVisualization = new List<Transform>();
    }

    private void Start()
    {
        _playerLimbs = SystemManager.Instance.playerGameObject.GetComponentInChildren<PlayerController>().VisionTargetsAI;
        _numberVisionTargets = _playerLimbs.Length;
    }

    private void Update()
    {
        PlayerVisible();
    }

    private void PlayerVisible()
    {
        _playerVisualization.Clear();
        Collider[] playerCollider = new Collider[1];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, viewData.ViewRadius, playerCollider, playerMask); 
        if (numColliders == 0)
            return;
        
        Transform player = playerCollider[0].transform;
        Vector3 dirToTarget = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
        {
            _hitCount = 0;
            for(int i = 0; i < _numberVisionTargets; i++)
            {
                var playerDir = _playerLimbs[i].position - transform.position;
                NavMeshHit hit;

                if (!_enemy.Agent.Raycast(_playerLimbs[i].position, out hit))
                {
                    _hitCount++;
                    _playerVisualization.Add(_playerLimbs[i]);
                }
            }

            if (_hitCount > 0)
            {
                EnemyVisionData visionData = new EnemyVisionData(player.position);
                _enemy.PlayerSpotted(visionData);

            }
        }
    }
    
    public Vector3 AngleToDir(float angle, bool global)
    {
        if (!global)
            angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public float ViewRadius => viewData.ViewRadius;
    public float ViewAngle => viewData.ViewAngle;
    public List<Transform> PlayerVisualization => _playerVisualization;
}
