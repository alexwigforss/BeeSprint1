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
	Vector3 moveForce = Vector3.zero;
	Vector3 rotateForce = Vector3.zero;
	Boolean accelerate = false;
	Boolean reverse = false;
	Boolean ascend = false;
	Boolean descend = false;
	Boolean strleft = false;
	Boolean strright = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (rb != null)
		{
			// Apply rotation
			//rb.transform.Rotate(rotateForce);
			rb.transform.Rotate(rotateForce * Time.deltaTime);

			// Apply movement
			Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			rb.velocity += worldMoveForce * Time.deltaTime;
		}

		if (accelerate && fwdspeed < 10.0f) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
		else if (!accelerate && fwdspeed > 0.0f) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
		if (reverse && fwdspeed > -10.0f) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
		else if (!reverse && fwdspeed < 0.0f) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
		if (fwdspeed > -0.1 && fwdspeed < 0.1) { fwdspeed = moveForce.z = 0.0f; }

		if (ascend && risespeed < 10.0f) { risespeed += acceleration; moveForce.y = risespeed; }
		else if (!ascend && risespeed > 0.0f) { risespeed -= acceleration; moveForce.y = risespeed; }
		if (descend && risespeed > -10.0f) { risespeed -= acceleration; moveForce.y = risespeed; }
		else if (!descend && risespeed < 0.0f) { risespeed += acceleration; moveForce.y = risespeed; }
		if (risespeed > -0.1 && risespeed < 0.1) { risespeed = moveForce.y = 0.0f; }
	}

	public void HandleX()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseDelta.x / 10, 0f));
	}
	public void HandleY()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(mouseDelta.y / 10, 0f, 0f));
	}

	public void HandleMoveUp(InputAction.CallbackContext context)
	{
		if (context.performed) { ascend = true; }
		else if (context.canceled) { ascend = false; }
	}
	public void HandleMoveDown(InputAction.CallbackContext context)
	{
		if (context.performed) { descend = true; }
		else if (context.canceled) { descend = false; }
	}

	public void HandleMoveRight(InputAction.CallbackContext context)
	{
		//if (context.performed) { rotateForce.y += rotateSpeed; }
		//else if (context.canceled) { rotateForce.y = 0; }
		// strife
		if (context.performed) { moveForce.x = moveSpeed * 10; }
		else if (context.canceled) { moveForce.x = 0; }
	}

	public void HandleMoveLeft(InputAction.CallbackContext context)
	{
		//if (context.performed) { rotateForce.y -= rotateSpeed; }
		//else if (context.canceled) { rotateForce.y = 0; }
		// strife
		if (context.performed) { moveForce.x = -moveSpeed * 10; }
		else if (context.canceled) { moveForce.x = 0; }
	}

	// TODO Bygg ut eventsystemet med 
	public void HandleSpinRight(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.z -= rotateSpeed; }
		else if (context.canceled) { rotateForce.z = 0; }
	}

	public void HandleSpinLeft(InputAction.CallbackContext context)
	{
		if (context.performed) { rotateForce.z += rotateSpeed; }
		else if (context.canceled) { rotateForce.z = 0; }
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
}
