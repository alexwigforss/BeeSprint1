using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class TopDownCamMove : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField] float moveSpeed;
	//float fwdspeed, strspeed, risespeed = 0.0f;
	//float maxSpeed = 50.0f;
	Vector3 moveForce = Vector3.zero;
	//Vector3 rotateForce = Vector3.zero;
	//Boolean accelerate = false;
	//Boolean reverse = false;
	//Boolean ascend = false;
	//Boolean descend = false;
	//Boolean strleft = false;
	//Boolean strright = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (rb != null)
		{
			// Apply rotation
			// rb.transform.Rotate(rotateForce);
			// rb.transform.Rotate(rotateForce * Time.deltaTime);

			// Apply movement
			Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			rb.velocity += worldMoveForce * Time.deltaTime;

			//// Apply movement with Acceleration / Deceleration
			//if (accelerate && fwdspeed < maxSpeed) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
			//else if (!accelerate && fwdspeed > 0.0f) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
			//if (reverse && fwdspeed > -maxSpeed) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
			//else if (!reverse && fwdspeed < 0.0f) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
			//if (fwdspeed > -0.1 && fwdspeed < 0.1) { fwdspeed = moveForce.z = 0.0f; }

			//// Apply movement with Ascending / Descending
			//if (ascend && risespeed < maxSpeed / 2) { risespeed += acceleration; moveForce.y = risespeed; }
			//else if (!ascend && risespeed > 0.0f) { risespeed -= acceleration; moveForce.y = risespeed; }
			//if (descend && risespeed > -maxSpeed / 2) { risespeed -= acceleration; moveForce.y = risespeed; }
			//else if (!descend && risespeed < 0.0f) { risespeed += acceleration; moveForce.y = risespeed; }
			//if (risespeed > -0.1 && risespeed < 0.1) { risespeed = moveForce.y = 0.0f; }
		}

	}
	/*
	public void HandleMouseX()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseDelta.x / 10, 0f));
	}
	public void HandleMouseY()
	{
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(mouseDelta.y / 10, 0f, 0f));
	}*/
	public void HandleCamLeft(InputAction.CallbackContext context)
	{
		Debug.Log("Left");
		//if (context.performed) { moveForce.x = moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamRight(InputAction.CallbackContext context)
	{
		Debug.Log("Right");
		//if (context.performed) { moveForce.x = -moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamUp(InputAction.CallbackContext context)
	{
		Debug.Log("Up");
		//if (context.performed) { moveForce.x = moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamDown(InputAction.CallbackContext context)
	{
		Debug.Log("Down");
		//if (context.performed) { moveForce.x = -moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamRotateCounterClock(InputAction.CallbackContext context)
	{
		Debug.Log("CC");
		//if (context.performed) { moveForce.x = moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamRotateClockwise(InputAction.CallbackContext context)
	{
		Debug.Log("CW");
		//if (context.performed) { moveForce.x = -moveSpeed * 20; }
		//else if (context.canceled) { moveForce.x = 0; }
	}

	/*
		public void HandleStrifeRight(InputAction.CallbackContext context)
		{
			if (context.performed) { moveForce.x = moveSpeed * 20; }
			else if (context.canceled) { moveForce.x = 0; }	}
		public void HandleStrifeLeft(InputAction.CallbackContext context)
		{
			if (context.performed) { moveForce.x = -moveSpeed * 20; }
			else if (context.canceled) { moveForce.x = 0; }
		}
	*/
	//public void HandleMoveForward(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { accelerate = true; }
	//	else if (context.canceled) { accelerate = false; }
	//}
	//public void HandleMoveBackward(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { reverse = true; }
	//	else if (context.canceled) { reverse = false; }
	//}
	//public void HandleTiltBack(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { rotateForce.x -= rotateSpeed; }
	//	else if (context.canceled) { rotateForce.x = 0; }
	//}
	//public void HandleTiltLeft(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { rotateForce.z += rotateSpeed; }
	//	else if (context.canceled) { rotateForce.z = 0; }
	//}
	//public void HandleTiltForward(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { rotateForce.x += rotateSpeed; }
	//	else if (context.canceled) { rotateForce.x = 0; }
	//}
	//public void HandleTiltRight(InputAction.CallbackContext context)
	//{
	//	if (context.performed) { rotateForce.z -= rotateSpeed; }
	//	else if (context.canceled) { rotateForce.z = 0; }
	//}
	//public void HandleRealign(InputAction.CallbackContext context)
	//{
	//	if (context.performed)
	//	{
	//		Debug.Log(rb.transform);
	//	}
	//}
}
