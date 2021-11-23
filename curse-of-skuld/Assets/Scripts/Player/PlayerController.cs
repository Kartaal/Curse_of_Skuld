using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Camera _camera;

    private Animator _moveAnim;

    private Vector3 _movementVector;

    private float _currentSpeed;

    private CharacterController _charController;
    // private GameObject _collisionDetector;

    [SerializeField] private bool _controllerLocked = false;

    
    private bool _canSprint;
    private float _tempMaxSpeed;

    private bool _dead;
    
    
    private void Awake()
    {
        _dead = false;
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _moveAnim = GetComponentInChildren<Animator>();
        Vector3 moveVec = transform.forward + Vector3.down * 30f;
        _charController.Move(moveVec);
        _camera = Camera.main;
        
        _tempMaxSpeed = playerData.MaxSpeed;
        _canSprint = false;
    }

    void Update()
    {
        if (_controllerLocked)
            return;

        float targetSpeed = _movementVector == Vector3.zero ? 0 : _tempMaxSpeed;
        
        if(_moveAnim != null)
            _moveAnim.SetBool("IsMoving", _movementVector != Vector3.zero);

        Vector3 moveVec = _camera.transform.TransformDirection(_movementVector);
        moveVec.y = 0;
        moveVec.Normalize();

        // a reference to the players current horizontal velocity
        float speedOffset = 0.1f;
        // accelerate or decelerate to target speed
        if (_currentSpeed < targetSpeed - speedOffset || _currentSpeed > targetSpeed + speedOffset)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * playerData.SpeedChangeRate);
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(_angle + _camera.transform.eulerAngles.y, Vector3.up), Time.deltaTime * playerData.RotationSpeed);
        }
        
        //Sprint
        if (StaminaManager.Instance.currentStamina != 0&& _canSprint )
        {
            _tempMaxSpeed = playerData.SprintMaxSpeed;
            _moveAnim.SetFloat("Speed",playerData.SprintAnimSpeedMultiplier);
        }
        else
        {
            _tempMaxSpeed = playerData.MaxSpeed;
            _moveAnim.SetFloat("Speed",1);
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
        var childRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var mesh in childRenderers)
        {
            mesh.enabled = false;
        }

        enabled = false;
        _dead = true;
        StartCoroutine(GoToDeathScreen());
    }


    IEnumerator GoToDeathScreen()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Scene_Lose");
    }
    public void ToggleControllerLocked()
    {
        _controllerLocked = !_controllerLocked;
        _moveAnim.SetBool("IsMoving", false);
    }

    public void MoveTo(Vector3 position, Vector3 lookAt)
    {
        //character controller resets the position, so it needs to be disabled to set it
        _charController.enabled = false;
        this.transform.position = position;
        this.transform.LookAt(lookAt);
        _charController.enabled = true;
    }

    public bool Dead => _dead;
}
