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
    private float maxXAngle;
    [SerializeField]
    private float minXAngle;
    [SerializeField]
    private float speed;

    private float _y = 0;
    private float _x = 0;

    private Vector2 _mouseMovement;
    void FixedUpdate () {
        print("in here");
        _y += speed * _mouseMovement.x;
        _x += speed * _mouseMovement.y;
        
        // float y = 5 * -Input.GetAxis ("Mouse Y");
        // _y = Mathf.Repeat(_y, 360);
        _y = Mathf.Clamp(_y, minYAngle, maxYAngle);
        _x = Mathf.Clamp(_x, minXAngle, maxXAngle);
        
        //this.transform.Rotate (0, x, 0);
        transform.rotation=Quaternion.Euler(-_x,_y,0);
        
     
    }

    public void OnLook(InputValue input)
    {
        _mouseMovement = input.Get<Vector2>();
        
    }
}
