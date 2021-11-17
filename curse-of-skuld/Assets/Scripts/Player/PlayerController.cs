using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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

    [SerializeField]
    private Animator moveAnim;

    private Vector3 _movementVector;

    private float _currentSpeed;

    private CharacterController _charController;
    // private GameObject _collisionDetector;

    [SerializeField] private bool _controllerLocked = false;

    [FormerlySerializedAs("sprintActualSpeed")]
    [Header("SprintSetting")] 
    [SerializeField] private float sprintMovementSpeed;
    [SerializeField] private float sprintAnimSpeedMultiplier;
    private bool _canSprint;
    private float _tempMaxSpeed;
    
    
    private void Awake()
    {
        // interactDetection.enabled = false;
       
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        Vector3 moveVec = transform.forward + Vector3.down * 30f;
        _charController.Move(moveVec);
        
        // probably needs change
        _tempMaxSpeed = maxSpeed;
        _canSprint = false;
    }

    void Update()
    {
        if (_controllerLocked)
            return;

        float targetSpeed = _movementVector == Vector3.zero ? 0 : maxSpeed;
        
        if(moveAnim != null)
            moveAnim.SetBool("IsMoving", _movementVector != Vector3.zero);

        Vector3 moveVec = camera.transform.TransformDirection(_movementVector);
        moveVec.y = 0;
        moveVec.Normalize();

        // a reference to the players current horizontal velocity
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
        moveVec *= _currentSpeed;
        moveVec.y = -9f;
        _charController.Move(moveVec * Time.deltaTime);

        if (targetSpeed != 0)
        {
            float _angle = Mathf.Atan2(_movementVector.x, _movementVector.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(_angle + camera.transform.eulerAngles.y, Vector3.up), Time.deltaTime * rotationSpeed);
        }
        
        //Sprint
        if (StaminaManager.Instance.currentStamina != 0&& _canSprint )
        {
            maxSpeed = sprintMovementSpeed;
            moveAnim.SetFloat("Speed",sprintAnimSpeedMultiplier);
        }
        else
        {
            maxSpeed = _tempMaxSpeed;
            moveAnim.SetFloat("Speed",1);
        }
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

    public void OnStartSprint()
    {
        StaminaManager.Instance.StartDecreaseStamina();
        _canSprint = true;
    }

    public void OnStopSprint()
    {
        _canSprint = false;
        StaminaManager.Instance.StopDecreasingStamina();
    }
    
    public void Die()
    {
        print("Died");
        var childRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in childRenderers)
        {
            mesh.enabled = !mesh.enabled;
        }

        gameObject.GetComponent<PlayerController>().enabled = false;
        // Destroy(gameObject);
    }

    public void ToggleControllerLocked()
    {
        _controllerLocked = !_controllerLocked;
    }

    public void MoveTo(Vector3 position, Vector3 lookAt)
    {
        //character controller resets the position, so it needs to be disabled to set it
        _charController.enabled = false;
        this.transform.position = position;
        this.transform.LookAt(lookAt);
        _charController.enabled = true;
    }
}
