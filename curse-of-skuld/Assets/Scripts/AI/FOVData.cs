using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/FieldOfView")]
public class FOVData : ScriptableObject
{
    [SerializeField] private float viewRadius;
    [Range(0,360),SerializeField] private float viewAngle;
    
    [SerializeField] private float chaseViewRadius;
    [Range(0,360),SerializeField] private float chaseViewAngle;

    public float ViewAngle => viewAngle;

    public float ViewRadius => viewRadius;

    public float ChaseViewRadius => chaseViewRadius;

    public float ChaseViewAngle => chaseViewAngle;
    
}
