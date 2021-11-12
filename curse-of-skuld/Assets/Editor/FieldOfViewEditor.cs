using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        var pos = fov.transform.position;
        Handles.color = Color.white;
        Handles.DrawWireArc(pos, Vector3.up, Vector3.forward, 360, fov.ViewRadius);

        Vector3 viewAngleA = fov.AngleToDir(-fov.ViewAngle / 2, false);
        Vector3 viewAngleB = fov.AngleToDir(fov.ViewAngle / 2, false);


        Handles.DrawLine(pos, pos + viewAngleA * fov.ViewRadius);
        Handles.DrawLine(pos, pos + viewAngleB * fov.ViewRadius);
        
        Handles.color = Color.red;
        if(fov.PlayerVisualization != null)
            Handles.DrawLine(pos, fov.PlayerVisualization.position);

    }
}
