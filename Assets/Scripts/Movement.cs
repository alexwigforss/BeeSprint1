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
	[SerializeField] float acceleration;
	[SerializeField] float moveSpeed;
	[SerializeField] float rotateSpeed;
	float fwdspeed, strspeed, risespeed = 0.0f;
	float maxSpeed = 50.0f;
	Vector3 moveForce = Vector3.zero;
	Vector3 rotateForce = Vector3.zero;
	Boolean accelerate = false;
	Boolean reverse = false;
	Boolean ascend = false;
	Boolean descend = false;
	Boolean strleft = false;
	Boolean strright = false;
	Boolean strfleft = false;
	Boolean strfright = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (rb != null)
		{
			// Apply rotation
			rb.transform.Rotate(rotateForce * Time.deltaTime);

			// Apply movement
			Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			rb.velocity += worldMoveForce * Time.deltaTime;

			// Apply movement with Acceleration / Deceleration
			if (accelerate) { fwdspeed = Mathf.Min(fwdspeed + acceleration, maxSpeed); }
			else if (reverse) { fwdspeed = Mathf.Max(fwdspeed - acceleration, -maxSpeed); }
			else { fwdspeed = Mathf.MoveTowards(fwdspeed, 0, acceleration); }
			moveForce.z = fwdspeed;

			// Apply Steering with strleft / strright
			if (strleft) { strspeed = Mathf.Min(strspeed + rotateSpeed, maxSpeed); }
			else if (strright) { strspeed = Mathf.Max(strspeed - rotateSpeed, -maxSpeed); }
			else { strspeed = Mathf.MoveTowards(strspeed, 0, rotateSpeed); }
			rotateForce.y = strspeed;

			// Apply movement with Ascending / Descending
			if (ascend) { risespeed = Mathf.Min(risespeed + acceleration, maxSpeed / 2); }
			else if (descend) { risespeed = Mathf.Max(risespeed - acceleration, -maxSpeed / 2); }
			else { risespeed = Mathf.MoveTowards(risespeed, 0, acceleration); }
			moveForce.y = risespeed;

			// Apply Strifing with strfleft / strfright
			if (strfleft) { moveForce.x = -moveSpeed * 20; }
			else if (strfright) { moveForce.x = moveSpeed * 20; }
			else { moveForce.x = 0; }
		}
	}

	public void HandleMouseX()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseDelta.x / 10, 0f));
	}
	public void HandleMouseY()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(mouseDelta.y / 10, 0f, 0f));
	}
	public void HandleAscend(InputAction.CallbackContext context)
	{
		if (context.performed) { ascend = true; }
		else if (context.canceled) { ascend = false; }
	}
	public void HandleDescend(InputAction.CallbackContext context)
	{
		if (context.performed) { descend = true; }
		else if (context.canceled) { descend = false; }
	}
	public void HandleSteerRight(InputAction.CallbackContext context)
	{
		if (context.performed) { strleft = true; }
		else if (context.canceled) { strleft = false; }
	}
	public void HandleSteerLeft(InputAction.CallbackContext context)
	{
		if (context.performed) { strright = true; }
		else if (context.canceled) { strright = false; }
	}
	public void HandleStrifeRight(InputAction.CallbackContext context)
	{
		if (context.performed) { strfright = true; }
		else if (context.canceled) { strfright = false; }
	}
	public void HandleStrifeLeft(InputAction.CallbackContext context)
	{
		if (context.performed) { strfleft = true; }
		else if (context.canceled) { strfleft = false; }
	}
	public void HandleMoveForward(InputAction.CallbackContext context)
	{
		if (context.performed) { accelerate = true; }
		else if (context.canceled) { accelerate = false; }
	}
	public void HandleMoveBackward(InputAction.CallbackContext context)
	{
		if (context.performed) { reverse = true; }
		else if (context.canceled) { reverse = false; }
	}
	public void HandleTiltBack(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.x -= rotateSpeed * 10; }
		else if (context.canceled) { rotateForce.x = 0; }
	}
	public void HandleTiltLeft(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.z += rotateSpeed * 10; }
		else if (context.canceled) { rotateForce.z = 0; }
	}
	public void HandleTiltForward(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.x += rotateSpeed * 10; }
		else if (context.canceled) { rotateForce.x = 0; }
	}
	public void HandleTiltRight(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.z -= rotateSpeed * 10; }
		else if (context.canceled) { rotateForce.z = 0; }
	}
	public void HandleRealign(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log(rb.transform);
		}
	}
}
