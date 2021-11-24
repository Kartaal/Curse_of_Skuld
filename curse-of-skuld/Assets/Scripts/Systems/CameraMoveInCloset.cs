using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMoveInCloset : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float maxYAngle; 
    [SerializeField]
    private float minYAngle;
    [SerializeField]
    private float maxXAnagle;
    [SerializeField]
    private float minXAngle;
    [SerializeField]
    private float speed;

    private float _y = 0;
    private float _x = 0;
    void FixedUpdate () {
        
        _y += speed * Input.GetAxis ("Mouse X");
        _x -= speed * Input.GetAxis ("Mouse Y");
        // float y = 5 * -Input.GetAxis ("Mouse Y");
        // _y = Mathf.Repeat(_y, 360);
        _y = Mathf.Clamp(_y, minYAngle, maxYAngle);
        _x = Mathf.Clamp(_x, minXAngle, maxYAngle);
        
        //this.transform.Rotate (0, x, 0);
        transform.rotation=Quaternion.Euler(_x,_y,0);
        
     
    }
}