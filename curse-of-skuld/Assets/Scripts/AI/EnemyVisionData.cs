using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyVisionData
{
    private Vector3 _lastKnownPosition;
    private float _visibilityPercentage;

    public EnemyVisionData(Vector3 lastKnownPosition, float visibilityPercentage)
    {
        this._lastKnownPosition = lastKnownPosition;
        this._visibilityPercentage = visibilityPercentage;
    }

    public Vector3 LastKnownPosition => _lastKnownPosition;
    public float VisibilityPercentage => _visibilityPercentage;
}
