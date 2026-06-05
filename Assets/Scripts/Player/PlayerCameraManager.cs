using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.UIElements;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float _distanceToPlayer;

    private Vector2 _Input;

    [SerializeField]private mouseSens mouseSensitivity;
    private CameraRotation camRotation;
    [SerializeField]private CameraAngle cameraAngle;

    void Awake() => _distanceToPlayer = Vector3.Distance(transform.position, target.position);

    public void look(InputAction.CallbackContext context)
    {
        _Input = context.ReadValue<Vector2>();
    }
    void Update()
    {
        camRotation.yaw += _Input.x * mouseSensitivity.horizontal * Time.deltaTime;
        camRotation.pitch += _Input.y * mouseSensitivity.vertical * Time.deltaTime;
        camRotation.pitch = Mathf.Clamp(camRotation.pitch, cameraAngle.min, cameraAngle.max);
    }


    void LateUpdate()
    {
        transform.eulerAngles = new Vector3(camRotation.pitch, camRotation.yaw, 0.0f);
        transform.position = target.position - transform.forward * _distanceToPlayer;
    }

}

[Serializable]
public struct mouseSens
{
    public float horizontal;
    public float vertical;
}

public struct CameraRotation
{
    public float pitch;
    public float yaw;
}

[Serializable]
public struct CameraAngle
{
    public float max;
    public float min;
}