using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float speedChangeRate;
    

    [SerializeField]
    private Camera camera;

    

    private Vector3 _movementVector;

    private CharacterController _charController;

    private float _currentSpeed;


    private void Awake()
    {
       
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        Vector3 moveVec = transform.forward + Vector3.down * 30f;
        _charController.Move(moveVec);

    }

    void Update()
    {
        float targetSpeed = _movementVector == Vector3.zero ? 0 : maxSpeed;

        Vector3 moveVec = camera.transform.TransformDirection(_movementVector);
        moveVec.y = -2f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_charController.velocity.x, 0.0f, _charController.velocity.z).magnitude;
        float speedOffset = 0.1f;
        // accelerate or decelerate to target speed
        if (_currentSpeed < targetSpeed - speedOffset || _currentSpeed > targetSpeed + speedOffset)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * speedChangeRate);
        }
        else
        {
            _currentSpeed = targetSpeed;
        }

        _charController.Move(moveVec * _currentSpeed * Time.deltaTime);


        float _angle = Mathf.Atan2(_movementVector.x, _movementVector.z) * Mathf.Rad2Deg;
        print(_angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(_angle + camera.transform.eulerAngles.y, Vector3.up), Time.deltaTime * rotationSpeed);
    }

    //It is important the method is named this way for the input system to find it.
    public void OnMovement(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        _movementVector = new Vector3(inputVec.x, 0, inputVec.y);
    }

    //It is important the method is named this way for the input system to find it.
    public void OnInteract()
    {
        CollisionDetector.Instance.InteractionKeyPressed();
    }

    public void OnDebug()
    {
        SystemManager.Instance.ResetScene();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
