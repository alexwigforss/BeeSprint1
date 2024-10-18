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
	Camera cam;
	[SerializeField] float moveSpeed;
	[SerializeField] float rotoSpeed;
	Vector3 moveForce = Vector3.zero;
	float rotoForce = 0.0f;

	void Start()
	{
		cam = GetComponent<Camera>();
		cam.transform.position = new Vector3(0, 50, 0);
	}
	private void Update()
	{
		if (cam != null)
		{
			// Apply movement
			Vector3 worldMoveForce = cam.transform.TransformDirection(moveForce) * Time.deltaTime;
			Vector3 worldRotateForce = new Vector3(0, 0, rotoForce) * Time.deltaTime;
			// Debug.Log(worldMoveForce);
			cam.transform.position += worldMoveForce;
			cam.transform.Rotate(worldRotateForce);
		}
	}
	public void HandleCamLeft(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Left");
			moveForce.x = -moveSpeed;
		}
		else if (context.canceled) { moveForce.x = 0; }

	}
	public void HandleCamRight(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Right");
			moveForce.x = moveSpeed;
		}
		else if (context.canceled) { moveForce.x = 0; }
	}
	public void HandleCamUp(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Up");
			moveForce.y = moveSpeed;
		}
		else if (context.canceled) { moveForce.y = 0; }
	}
	public void HandleCamDown(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Down");
			moveForce.y = -moveSpeed;
		}
		else if (context.canceled) { moveForce.y = 0; }
	}
	public void HandleZoomIn(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("In");
			moveForce.z = moveSpeed;
		}
		else if (context.canceled) { moveForce.z = 0; }
	}
	public void HandleZoomOut(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("Out");
			moveForce.z = -moveSpeed;
		}
		else if (context.canceled) { moveForce.z = 0; }
	}
	public void HandleCamRotateCounterClock(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("CC");
			rotoForce = -rotoSpeed;
		}
		else if (context.canceled) { rotoForce = 0; }
	}
	public void HandleCamRotateClockwise(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Debug.Log("CW");
			rotoForce = rotoSpeed;
		}
		else if (context.canceled) { rotoForce = 0; }
	}
}
