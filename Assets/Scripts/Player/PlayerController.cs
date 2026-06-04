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
    private Vector2 _Input;
    private CharacterController _CharacterController;
    private Vector3 _Direction;
    private float _currentVelocity;
    [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float speed;

    private float _PlayerGravity = -9.18f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _gravVelocity;

    void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyRotation();
        ApplyMovement();
    }


    void ApplyMovement()
    {
        _CharacterController.Move(_Direction * speed * Time.deltaTime);    
    }

    void ApplyGravity()
    {
        _gravVelocity += _PlayerGravity * Time.deltaTime;
    }
    
    void ApplyRotation()
    {
        if(_Input.sqrMagnitude == 00) return;

        var targetAngle = Mathf.Atan2(_Direction.x, _Direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
    }

    public void move(InputAction.CallbackContext context)
    {
        _Input = context.ReadValue<Vector2>();
        _Direction = new Vector3(_Input.x, 0.0f, _Input.y);
    }
}
