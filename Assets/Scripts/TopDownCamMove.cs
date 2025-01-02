using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class TopDownCamMove : MonoBehaviour {
	[SerializeField] float moveSpeed;
	[SerializeField] float rotoSpeed;
	Vector3 moveForce = Vector3.zero;
	float rotoForce = 0.0f;

	void Start() {
		// Set the initial position of the parent GameObject
		transform.parent.position = new Vector3(0, 45, 0);
	}

	private void Update() {
		// Apply movement in the parent's local space for x and z axes
		Vector3 localMoveForce = new Vector3(moveForce.x, 0, moveForce.z) * Time.deltaTime;
		transform.parent.Translate(localMoveForce, Space.Self);

		// Apply movement in world space for y-axis (zoom)
		Vector3 worldMoveForce = new Vector3(0, moveForce.y, 0) * Time.deltaTime;
		transform.parent.position += worldMoveForce;

		// Apply rotation around the parent's local y-axis
		if (rotoForce != 0) {
			transform.parent.Rotate(0, rotoForce * Time.deltaTime, 0, Space.Self);
		}
	}

	public void HandleCamLeft(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.x = -moveSpeed;
		} else if (context.canceled) { moveForce.x = 0; }
	}

	public void HandleCamRight(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.x = moveSpeed;
		} else if (context.canceled) { moveForce.x = 0; }
	}

	public void HandleCamUp(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.z = moveSpeed;
		} else if (context.canceled) { moveForce.z = 0; }
	}

	public void HandleCamDown(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.z = -moveSpeed;
		} else if (context.canceled) { moveForce.z = 0; }
	}

	public void HandleZoomIn(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.y = -moveSpeed; // Move down in world space
		} else if (context.canceled) { moveForce.y = 0; }
	}

	public void HandleZoomOut(InputAction.CallbackContext context) {
		if (context.performed) {
			moveForce.y = moveSpeed; // Move up in world space
		} else if (context.canceled) { moveForce.y = 0; }
	}

	public void HandleCamRotateCounterClock(InputAction.CallbackContext context) {
		if (context.performed) {
			rotoForce = -rotoSpeed;
		} else if (context.canceled) { rotoForce = 0; }
	}

	public void HandleCamRotateClockwise(InputAction.CallbackContext context) {
		if (context.performed) {
			rotoForce = rotoSpeed;
		} else if (context.canceled) { rotoForce = 0; }
	}
}
