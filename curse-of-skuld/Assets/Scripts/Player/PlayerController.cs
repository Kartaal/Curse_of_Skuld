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
    private GameObject _collisionDetector;

    void Start()
    {
        _charController = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (_movementVector == Vector3.zero) return;

        _charController.Move(transform.forward * speed * Time.deltaTime);

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
        //make a reference to the collisionDetection sensor that is attached to the main character and then switch it on or off here with each E pressed
        //collisiondetection.setactive
        print("in interact");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
