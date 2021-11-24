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
    private Transform _playerVisualization;

    private Transform[] _playerLimbs;
    private int _hitCount;
    private int _numberVisionTargets;
    NavMeshHit _hit;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _hitCount = 0;
        _playerLimbs = SystemManager.Instance.playerGameObject.GetComponent<PlayerController>().VisionTargetsAI;
        _numberVisionTargets = _playerLimbs.Length;
    }

    private void Update()
    {
        PlayerVisible();
        
    }

    private void PlayerVisible()
    {
        _playerVisualization = null;
        Collider[] playerCollider = new Collider[1];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, viewData.ViewRadius, playerCollider, playerMask); 
        if (numColliders == 0)
            return;
        
        Transform player = playerCollider[0].transform;
        Vector3 dirToTarget = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
        {
            _hitCount = 0;
            var playerDir = player.position - transform.position;
            for(int i = 0; i < _numberVisionTargets; i++)
            {
                if (!Physics.Raycast(_enemy.transform.position, playerDir, viewData.ViewRadius, playerMask))
                {
                    _hitCount++;
                }
            }

            if (_hitCount > 0)
            {
                EnemyVisionData visionData = new EnemyVisionData(player.position, _hitCount/_numberVisionTargets);
                _playerVisualization = player;
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
    public Transform PlayerVisualization => _playerVisualization;
}
