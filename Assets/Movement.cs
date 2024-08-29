using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed;
    Vector3 moveForce = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (rb != null)
        {
            Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
            rb.velocity += worldMoveForce;

        }
    }

    public void HandleX()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseDelta.x/10, 0f));
    }
    public void HandleY()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(mouseDelta.y / 10, 0f, 0f));
    }

    public void HandleMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.y = moveSpeed; }
        else if (context.canceled) { moveForce.y = 0; }
    }
    public void HandleMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.y = -moveSpeed; }
        else if (context.canceled) { moveForce.y = 0; }
    }
    public void HandleMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.x = moveSpeed; }
        else if (context.canceled) { moveForce.x = 0; }
    }
    public void HandleMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.x = -moveSpeed; }
        else if (context.canceled) { moveForce.x = 0; }
    }
    public void HandleMoveForward(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.z = moveSpeed * 2; }
        else if (context.canceled) { moveForce.z = 0; }
    }
    public void HandleMoveBackward(InputAction.CallbackContext context)
    {
        if (context.performed) { moveForce.z = -moveSpeed * 2; }
        else if (context.canceled) { moveForce.z = 0; }
    }
}
