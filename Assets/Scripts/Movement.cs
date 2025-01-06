using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
	Rigidbody rb;
	[SerializeField] float acceleration;
	[SerializeField] float moveSpeed;
	[SerializeField] float rotateSpeed;
	float fwdspeed, strspeed, risespeed = 0.0f;
	float maxSpeed = 50.0f;
	Vector3 moveForce = Vector3.zero;
	Vector3 rotateForce = Vector3.zero;
	bool accelerate = false, reverse = false, ascend = false, descend = false, steerleft = false, steerright = false, strfleft = false, strfright = false;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		if (rb != null) {
			// Apply movement with Acceleration / Deceleration
			if (accelerate) { fwdspeed = Mathf.Min(fwdspeed + acceleration, maxSpeed); }
			else if (reverse) { fwdspeed = Mathf.Max(fwdspeed - acceleration, -maxSpeed); }
			else { fwdspeed = Mathf.MoveTowards(fwdspeed, 0, acceleration); }
			moveForce.z = fwdspeed;
			// Apply Steering with steerleft / steerright
			if (steerleft) { strspeed = Mathf.Min(strspeed + rotateSpeed, maxSpeed); }
			else if (steerright) { strspeed = Mathf.Max(strspeed - rotateSpeed, -maxSpeed); }
			else { strspeed = Mathf.MoveTowards(strspeed, 0, rotateSpeed); }
			rotateForce.y = strspeed;
			// Apply movement with Ascending / Descending
			if (ascend) { risespeed = Mathf.Min(risespeed + acceleration, maxSpeed / 2); }
			else if (descend) { risespeed = Mathf.Max(risespeed - acceleration, -maxSpeed / 2); }
			else { risespeed = Mathf.MoveTowards(risespeed, 0, acceleration); }
			moveForce.y = risespeed;
			// Apply Strafing with strfleft / strfright
			if (strfleft) { moveForce.x = -moveSpeed * 20; }
			else if (strfright) { moveForce.x = moveSpeed * 20; }
			else { moveForce.x = 0; }
			// Convert the movement force from local space to world space, making the motion relative to the object's orientation
			Vector3 localMoveForce = rb.transform.TransformDirection(moveForce);
			// Apply rotation, then movement
			rb.transform.Rotate(rotateForce * Time.deltaTime);
			rb.velocity += localMoveForce * Time.deltaTime;
		}
	}

	public void HandleMouseX() {
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseDelta.x / 10, 0f));
	}
	public void HandleMouseY() {
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(mouseDelta.y / 10, 0f, 0f));
	}
	public void HandleAscend(InputAction.CallbackContext context) {
		if (context.performed) { ascend = true; } else if (context.canceled) { ascend = false; }
	}
	public void HandleDescend(InputAction.CallbackContext context) {
		if (context.performed) { descend = true; } else if (context.canceled) { descend = false; }
	}
	public void HandleSteerRight(InputAction.CallbackContext context) {
		if (context.performed) { steerleft = true; } else if (context.canceled) { steerleft = false; }
	}
	public void HandleSteerLeft(InputAction.CallbackContext context) {
		if (context.performed) { steerright = true; } else if (context.canceled) { steerright = false; }
	}
	public void HandleStrafeRight(InputAction.CallbackContext context) {
		if (context.performed) { strfright = true; } else if (context.canceled) { strfright = false; }
	}
	public void HandleStrafeLeft(InputAction.CallbackContext context) {
		if (context.performed) { strfleft = true; } else if (context.canceled) { strfleft = false; }
	}
	public void HandleMoveForward(InputAction.CallbackContext context) {
		if (context.performed) { accelerate = true; } else if (context.canceled) { accelerate = false; }
	}
	public void HandleMoveBackward(InputAction.CallbackContext context) {
		if (context.performed) { reverse = true; } else if (context.canceled) { reverse = false; }
	}
	public void HandleTiltBack(InputAction.CallbackContext context) {
		if (context.performed) { rotateForce.x -= rotateSpeed * 10; } else if (context.canceled) { rotateForce.x = 0; }
	}
	public void HandleTiltLeft(InputAction.CallbackContext context) {
		if (context.performed) { rotateForce.z += rotateSpeed * 10; } else if (context.canceled) { rotateForce.z = 0; }
	}
	public void HandleTiltForward(InputAction.CallbackContext context) {
		if (context.performed) { rotateForce.x += rotateSpeed * 10; } else if (context.canceled) { rotateForce.x = 0; }
	}
	public void HandleTiltRight(InputAction.CallbackContext context) {
		if (context.performed) { rotateForce.z -= rotateSpeed * 10; } else if (context.canceled) { rotateForce.z = 0; }
	}
	public void HandleRealign(InputAction.CallbackContext context) {
		if (context.performed) {
			Debug.Log(rb.transform);
		}
	}
}
