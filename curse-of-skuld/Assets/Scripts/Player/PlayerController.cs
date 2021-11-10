using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float rotationSpeed = 2f;

    [SerializeField]
    private Camera camera;

    

    private Vector3 _movementVector;

    private CharacterController _charController;
    // private GameObject _collisionDetector;

    private void Awake()
    {
        // interactDetection.enabled = false;
       
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        Vector3 moveVec = transform.forward + Vector3.down * 30f;
        _charController.Move(moveVec);

    }

    void Update()
    {
        if (_movementVector == Vector3.zero) return;

        Vector3 moveVec = transform.forward + Vector3.down * 9f;
        _charController.Move(moveVec * speed * Time.deltaTime);

        float _angle = Mathf.Atan2(_movementVector.x, _movementVector.z) * Mathf.Rad2Deg;
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
