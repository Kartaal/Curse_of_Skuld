using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PatrolWaypoint : MonoBehaviour
{
    [SerializeField] private Transform viewTarget;
    [SerializeField] private float waitTime=0;
    private Vector3 _viewDir;

    private void Awake()
    {
        _viewDir = viewTarget.position;
    }

    public Transform PatrolPoint => transform;

    public Vector3 ViewDir => _viewDir;

    public float WaitTime => waitTime;
}
