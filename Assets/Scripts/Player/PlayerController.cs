using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Player Input")]
    private Vector2 _Input;
    private CharacterController _CharacterController;
    
    
    [Header("Rotation and Speed Values")]
    private Vector3 _Direction;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float speed;

    
    [Header("Gravity Values")]
    private float _PlayerGravity = -9.18f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _gravVelocity;
    [SerializeField] private float jumpPower;


    private Camera _mainCamera;


    void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;    
    }

    void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }


    void ApplyMovement()
    {
        _CharacterController.Move(_Direction * speed * Time.deltaTime);    
    }

    
    void ApplyRotation()
    {
        if(_Input.sqrMagnitude == 00) return;

        _Direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_Input.x, 0f, _Input.y);
        var targetRotation = Quaternion.LookRotation(_Direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (isGrounded() && _gravVelocity < 0.0f)
        {
            _gravVelocity = -1.0f;
        }
        else
            _gravVelocity += _PlayerGravity * gravityMultiplier * Time.deltaTime;
         _Direction.y = _gravVelocity;
    }

    public void move(InputAction.CallbackContext context)
    {
        _Input = context.ReadValue<Vector2>();
        _Direction = new Vector3(_Input.x, 0.0f, _Input.y);
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!isGrounded()) return;

        _gravVelocity += jumpPower;


    }

    private bool isGrounded() => _CharacterController.isGrounded;

}
