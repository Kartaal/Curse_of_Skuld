using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/BasicEnemy")]

public class EnemyData : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;

    public float MoveSpeed => moveSpeed;

    public float ChaseSpeed => chaseSpeed;
}
