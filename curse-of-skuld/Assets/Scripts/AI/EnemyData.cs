using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/BasicEnemy")]

public class EnemyData : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float killDistance;
    [SerializeField] private float searchDuration;

    public float MoveSpeed => moveSpeed;

    public float ChaseSpeed => chaseSpeed;

    public float KillDistance => killDistance;

    public float SearchDuration => searchDuration;
}
