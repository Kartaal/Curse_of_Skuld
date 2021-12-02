using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyVisionData
{
    private Vector3 _lastKnownPosition;

    public EnemyVisionData(Vector3 lastKnownPosition)
    {
        this._lastKnownPosition = lastKnownPosition;
    }

    public Vector3 LastKnownPosition => _lastKnownPosition;
}
