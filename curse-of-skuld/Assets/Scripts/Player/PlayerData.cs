using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField]private float maxSpeed;

    [SerializeField]private float rotationSpeed;

    [SerializeField]private float speedChangeRate;

    [FormerlySerializedAs("sprintActualSpeed")]
    [Header("SprintSetting")]
    [SerializeField] private float sprintMaxSpeed;
    [SerializeField] private float sprintAnimSpeedMultiplier;

    public float MaxSpeed => maxSpeed;

    public float RotationSpeed => rotationSpeed;

    public float SpeedChangeRate => speedChangeRate;

    public float SprintMaxSpeed => sprintMaxSpeed;

    public float SprintAnimSpeedMultiplier => sprintAnimSpeedMultiplier;
}

