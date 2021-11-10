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

    [SerializeField] private Collider interactDetection;

    private Vector3 _movementVector;

    private CharacterController _charController;
    private GameObject _collisionDetector;

    private void Awake()
    {
        interactDetection.enabled = false;
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
        print(_angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(_angle + camera.transform.eulerAngles.y, Vector3.up), Time.deltaTime * rotationSpeed);
        print("from " + transform.rotation.ToString() + " to " + Quaternion.AngleAxis(_angle + camera.transform.eulerAngles.y, Vector3.up).ToString());


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
        StartCoroutine(InteractDuration());
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator InteractDuration()
    {
        interactDetection.enabled = true;
        yield return new WaitForSeconds(0.5f);
        interactDetection.enabled = false;

    }
}
